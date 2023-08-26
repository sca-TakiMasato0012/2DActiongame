using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
    //public Animator animator;

    public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;



    //bool dashAxis = false;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, running, jumping, dashing;
    public string currentState;
    public string currentAnimation;
    public string previousState;
    public float movement;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacoterState(currentState);
    }

    public void SetCharacoterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        if(state.Equals("Running"))
        {
            SetAnimation(running, true, 1f);
        }
        if(state.Equals("Dashing")&&dash== true)
        {
            SetAnimation(dashing, true, 1f);
        }
    }
    
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)//アニメーションが再生さ入れているときはなにもしない
    {
        
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    // Update is called once per frame
    void Update () {

		//horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
       

        Mathf.Abs(horizontalMove);

		
		if (Input.GetKeyDown(KeyCode.Space))
		{
            
            jump = true;
            

        }

		if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
            

        }

        Move();

    }
    public void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(movement * runSpeed,rb.velocity.y);

    if(movement != 0)
        {
            if(!currentState.Equals("Jumping"))
            {
                SetCharacoterState("Running");
            }
            if (movement > 0)
            {
                transform.localScale = new Vector2(1f, 1f);

            }
            else
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
    else
        {
            SetCharacoterState("Idle");
        }
        

    }

	public void OnFall()
	{
		//animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		//animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
}
