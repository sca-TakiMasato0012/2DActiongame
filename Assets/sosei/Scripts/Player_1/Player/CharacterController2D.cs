using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Spine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // プレイヤーがジャンプするときに追加される力の量
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // 動きをどれだけ滑らかにするか
	[SerializeField] private bool m_AirControl = false;                         // プレイヤーがジャンプ中に操縦できるかどうか。
	[SerializeField] private LayerMask m_WhatIsGround;                          // キャラクターの基礎となるものを決定する
	[SerializeField] private Transform m_GroundCheck;                           // プレーヤーが接地しているかどうかを確認する位置を示すマーク。
	[SerializeField] private Transform m_WallCheck;								// あなたの人格をコントロールする姿勢を保ちます

	const float k_GroundedRadius = .2f; // 接地されているかどうかを判断するためのオーバーラップ円の半径
	private bool m_Grounded;            // プレーヤーが接地しているかどうか
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // プレイヤーが現在どの方向を向いているかを判断するため
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // 落下速度を制限する

	public bool canDoubleJump = true; //プレイヤーが二段ジャンプできる場合
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //プレイヤーがダッシュしている場合
	private bool m_IsWall = false; //プレイヤーの前に壁がある場合
	private bool isWallSliding = false; //プレイヤーが壁に滑り込んだ場合
	private bool oldWallSlidding = false; //プレーヤーが前のフレームで壁に滑り込んでいる場合
	private float prevVelocityX = 0f;
	private bool canCheck = false; //プレイヤーがウォールスライディングをしているかどうかを確認するため

	[SerializeField]
	public float Player_HP = 0;


	public float life = 10f; //プレイヤーのライフ
	public bool invincible = true; //プレイヤーが死ぬことができる場合
	private bool canMove = true; //プレイヤーが移動できる場合

	//private Animator animator;
	public ParticleSystem particleJumpUp; //粒子を追跡する
	public ParticleSystem particleJumpDown; //爆発粒子

	private float jumpWallStartX = 0;
	private float jumpWallDistX = 0; //プレイヤーと壁との距離
	private bool limitVelOnWallJump = false; //低いfpsで壁ジャンプ距離を制限する場合

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
	public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)//アニメーションが再生さ入れているときはなにもしない
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
                SetAnimation(dashing, false, 1f);//ダッシュする
            }
			// しゃがんでいる場合は、キャラクターが立ち上がることができるかどうかを確認してください
			if(isDashing)
			{
				m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * -m_DashForce, 0);
				SetAnimation(dashing, false, 1f);//ダッシュする
			}
			//地上にある場合、または airControl がオンになっている場合にのみプレーヤーを制御します

			else if (m_Grounded || m_AirControl)
			{

				if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);

				// 目標速度を求めてキャラクターを移動します
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// それを滑らかにしてキャラクターに適用します
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

				// 入力によってプレーヤーが右に移動し、プレーヤーが左を向いている場合...
				if(move > 0 && !m_FacingRight)
				{
					// ...プレーヤーを裏返します。
					Flip();
				}
				// それ以外の場合、入力がプレーヤーを左に動かし、プレーヤーが右を向いている場合...
				else if (move < 0 && m_FacingRight)
				{
					// ...プレーヤーを裏返します。
					Flip();
				}
			}
			// プレイヤーがジャンプしたら...
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

	private void OnCollisionEnter2D(Collision2D collision)//攻撃をくらったら
   {
		if(collision.gameObject.tag == "stone") {
			//PlayAnimation(After_Animation);//ひるむアニメーションを再生

			Player_HP = Player_HP - 1.0f;//くらったら１ダメージ
		}

		if(collision.gameObject.tag == "Goburin") {
			//PlayAnimation(After_Animation);//ひるむアニメーションを再生

			Player_HP = Player_HP - 1.0f;//矢をくらったら１ダメージ
		}
		if(collision.gameObject.tag == "Bat") {
			//PlayAnimation(After_Animation);//ひるむアニメーションを再生

			Player_HP = Player_HP - 1.0f;//矢をくらったら１ダメージ

	
		}
	}

	void Update() 
	{
		if(Player_HP  <= 0)//もし倒されたら
		{

			
			Destroy(gameObject);

		}
	}
}

