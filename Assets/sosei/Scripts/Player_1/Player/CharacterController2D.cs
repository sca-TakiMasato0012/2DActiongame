using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // �v���C���[���W�����v����Ƃ��ɒǉ������̗͂�
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // �������ǂꂾ�����炩�ɂ��邩
	[SerializeField] private bool m_AirControl = false;                         // �v���C���[���W�����v���ɑ��c�ł��邩�ǂ����B
	[SerializeField] private LayerMask m_WhatIsGround;                          // �L�����N�^�[�̊�b�ƂȂ���̂����肷��
	[SerializeField] private Transform m_GroundCheck;                           // �v���[���[���ڒn���Ă��邩�ǂ������m�F����ʒu�������}�[�N�B
	[SerializeField] private Transform m_WallCheck;								// ���Ȃ��̐l�i���R���g���[������p����ۂ��܂�

	const float k_GroundedRadius = .2f; // �ڒn����Ă��邩�ǂ����𔻒f���邽�߂̃I�[�o�[���b�v�~�̔��a
	private bool m_Grounded;            // �v���[���[���ڒn���Ă��邩�ǂ���
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // �v���C���[�����݂ǂ̕����������Ă��邩�𔻒f���邽��
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // �������x�𐧌�����

	public bool canDoubleJump = true; //�v���C���[����i�W�����v�ł���ꍇ
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //�v���C���[���_�b�V�����Ă���ꍇ
	private bool m_IsWall = false; //�v���C���[�̑O�ɕǂ�����ꍇ
	private bool isWallSliding = false; //�v���C���[���ǂɊ��荞�񂾏ꍇ
	private bool oldWallSlidding = false; //�v���[���[���O�̃t���[���ŕǂɊ��荞��ł���ꍇ
	private float prevVelocityX = 0f;
	private bool canCheck = false; //�v���C���[���E�H�[���X���C�f�B���O�����Ă��邩�ǂ������m�F���邽��

	[SerializeField]
	public float Player_HP = 0;


	public float life = 10f; //�v���C���[�̃��C�t
	public bool invincible = true; //�v���C���[�����ʂ��Ƃ��ł���ꍇ
	private bool canMove = true; //�v���C���[���ړ��ł���ꍇ

	//private Animator animator;
	public ParticleSystem particleJumpUp; //���q��ǐՂ���
	public ParticleSystem particleJumpDown; //�������q

	private float jumpWallStartX = 0;
	private float jumpWallDistX = 0; //�v���C���[�ƕǂƂ̋���
	private bool limitVelOnWallJump = false; //�Ⴂfps�ŕǃW�����v�����𐧌�����ꍇ

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, running,jumping, dashing;
    public string currentState;
    public string currentAnimation;
    public string previousState;

    [Header("Events")]
	[Space]

	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;

   
    
    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		
		if (OnFallEvent == null)
			OnFallEvent = new UnityEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}
	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)//�A�j���[�V�������Đ�������Ă���Ƃ��͂Ȃɂ����Ȃ�
	  {

		if(animation.name.Equals(currentAnimation)) {
			return;
		}
		Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
		animationEntry.TimeScale = timeScale;
		currentAnimation = animation.name;
	}


	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
				if (!wasGrounded )
				{
					OnLandEvent.Invoke();
					if (!m_IsWall && !isDashing) 
						particleJumpDown.Play();
					
				canDoubleJump = true;
					if (m_Rigidbody2D.velocity.y < 0f)
						limitVelOnWallJump = false;
			}
		}

		m_IsWall = false;

		if (!m_Grounded)
		{
			SetAnimation(jumping,false,1f);
			
        }

	}


	public void Move(float move, bool jump, bool dash)
	{
		if (canMove) 
		{
			
			if (dash && canDash)
			{
				m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
				StartCoroutine(DashCooldown());
                SetAnimation(dashing, false, 1f);//�_�b�V������
            }
			// ���Ⴊ��ł���ꍇ�́A�L�����N�^�[�������オ�邱�Ƃ��ł��邩�ǂ������m�F���Ă�������
			if(isDashing)
			{
				m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * -m_DashForce, 0);
				SetAnimation(dashing, false, 1f);//�_�b�V������
			}
			//�n��ɂ���ꍇ�A�܂��� airControl ���I���ɂȂ��Ă���ꍇ�ɂ̂݃v���[���[�𐧌䂵�܂�

			else if (m_Grounded || m_AirControl)
			{

				if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);

				// �ڕW���x�����߂ăL�����N�^�[���ړ����܂�
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// ��������炩�ɂ��ăL�����N�^�[�ɓK�p���܂�
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

				// ���͂ɂ���ăv���[���[���E�Ɉړ����A�v���[���[�����������Ă���ꍇ...
				if(move > 0 && !m_FacingRight)
				{
					// ...�v���[���[�𗠕Ԃ��܂��B
					Flip();
				}
				// ����ȊO�̏ꍇ�A���͂��v���[���[�����ɓ������A�v���[���[���E�������Ă���ꍇ...
				else if (move < 0 && m_FacingRight)
				{
					// ...�v���[���[�𗠕Ԃ��܂��B
					Flip();
				}
			}
			// �v���C���[���W�����v������...
			if(m_Grounded && jump)
			{
				SetAnimation(jumping,false,1f);
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				canDoubleJump = true;
				particleJumpDown.Play();
				particleJumpUp.Play();
			}
			else if (!m_Grounded && jump && canDoubleJump)
			{
				SetAnimation(jumping,false,1f);
				canDoubleJump = false;
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.2f));

			}
			else if (m_IsWall && !m_Grounded)
			{
				if (!oldWallSlidding && m_Rigidbody2D.velocity.y < 0 || isDashing)
				{
					isWallSliding = true;
					m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
					Flip();
					StartCoroutine(WaitToCheck(0.1f));
					canDoubleJump = true;
				}
				isDashing = false;

				if (isWallSliding)
				{
					if (move * transform.localScale.x > 0.1f)
					{
						StartCoroutine(WaitToEndSliding());
					}
					else 
					{
						oldWallSlidding = true;
						m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * 2, -5);
					}
				}

				if (jump && isWallSliding)
				{
					m_Rigidbody2D.velocity = new Vector2(0f, 0f);
					m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_JumpForce *1.2f, m_JumpForce));
					jumpWallStartX = transform.position.x;
					limitVelOnWallJump = true;
					canDoubleJump = true;
					isWallSliding = false;
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canMove = false;
				}
				else if (dash && canDash)
				{
					isWallSliding = false;
					
					
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canDoubleJump = true;
					StartCoroutine(DashCooldown());
				}
			}
			else if (isWallSliding && !m_IsWall && canCheck) 
			{
				isWallSliding = false;
				
				
				oldWallSlidding = false;
				m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
				canDoubleJump = true;
			}
        }
		
        
    }


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void ApplyDamage(float damage, Vector3 position) 
	{
		if (!invincible)
		{
			//animator.SetBool("Hit", true);
			life -= damage;
			Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f ;
			m_Rigidbody2D.velocity = Vector2.zero;
			m_Rigidbody2D.AddForce(damageDir * 10);
			if (life <= 0)
			{
				StartCoroutine(WaitToDead());
			}
			else
			{
				StartCoroutine(Stun(0.25f));
				StartCoroutine(MakeInvincible(1f));
			}


		}
	}

	IEnumerator DashCooldown()
	{
		
		SetAnimation(running, false, 1f);
		isDashing = true;
		canDash = false;
		yield return new WaitForSeconds(0.1f);
		isDashing = false;
		yield return new WaitForSeconds(0.5f);
		canDash = true;
	}

	IEnumerator Stun(float time) 
	{
		
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}
	IEnumerator MakeInvincible(float time) 
	{
		invincible = true;
		yield return new WaitForSeconds(time);
		invincible = false;
	}
	IEnumerator WaitToMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}

	IEnumerator WaitToCheck(float time)
	{
		canCheck = false;
		yield return new WaitForSeconds(time);
		canCheck = true;
	}

	IEnumerator WaitToEndSliding()
	{
		yield return new WaitForSeconds(0.1f);
		canDoubleJump = true;
		isWallSliding = false;
		
		oldWallSlidding = false;
		m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
	}

	IEnumerator WaitToDead()
	{
		
		canMove = false;
		invincible = true;
		GetComponent<Attack>().enabled = false;
		yield return new WaitForSeconds(0.4f);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		yield return new WaitForSeconds(1.1f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnCollisionEnter2D(Collision2D collision)//�U�������������
   {
		if(collision.gameObject.tag == "stone") {
			//PlayAnimation(After_Animation);//�Ђ�ރA�j���[�V�������Đ�

			Player_HP = Player_HP - 1.0f;//���������P�_���[�W
		}

		if(collision.gameObject.tag == "Goburin") {
			//PlayAnimation(After_Animation);//�Ђ�ރA�j���[�V�������Đ�

			Player_HP = Player_HP - 1.0f;//������������P�_���[�W
		}
		if(collision.gameObject.tag == "Bat") {
			//PlayAnimation(After_Animation);//�Ђ�ރA�j���[�V�������Đ�

			Player_HP = Player_HP - 1.0f;//������������P�_���[�W

	
		}
	}

	void Update() 
	{
		if(Player_HP  <= 0)//�����|���ꂽ��
		{

			
			Destroy(gameObject);

		}
	}
}

