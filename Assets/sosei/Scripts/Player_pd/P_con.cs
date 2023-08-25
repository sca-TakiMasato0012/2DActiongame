using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_con : MonoBehaviour
{
           
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle,run,jump,ken,yumi;
    public string currentState;
    public float speed;
    public float movement;
    private Rigidbody2D rigidbody;
    public string currentAnimation;
    public float JumpSpeed;
    public string previousState;


   

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
       currentState = "Idle";
        SetCharacoterState(currentState);
    }
        

    // Update is called once per frame
    void Update()
    {
        Move();
      
    }
    public void SetAnimation(AnimationReferenceAsset animation,bool loop, float timeScale) 
    {
        if(animation.name.Equals(currentAnimation)) {

            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;

    }

    private void AnimationEntry_Complete(TrackEntry trackEntry) 
    {
       if(currentState.Equals("Jump")) 
       {
            SetCharacoterState(previousState);

       }
    }

    public void SetCharacoterState(string state) 
    {
        

        if(state.Equals("Run")) 
        {
            SetAnimation(run,true,1f);

        }else if(state.Equals("Jump")) 
        {
            SetAnimation(jump, false, 1f);
        }
        else
        {
            SetAnimation(idle, true, 1f);
        }
            
        if(state.Equals("Ken")) 
        {
            SetAnimation(ken, false, 1f);

        }
        currentState = state;
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(movement * speed,rigidbody.velocity.y);
        if(movement != 0) 
        {
            if(!currentState.Equals("Jump")) 
            {
                SetCharacoterState("Run");
            }
            
            if(movement >0) 
            {
                transform.localScale = new Vector2(1f,1f);

            }
            else 
            {
                transform.localScale = new Vector2(-1f, 1f);

            }

        }else 
        {
            if(!currentState.Equals("Jump")) 
            {
                SetCharacoterState("Idle");
            }
                
        }

        if(Input.GetButtonDown("Jump")) 
        {
            Jump();

        }

        if(Input.GetKey(KeyCode.K)) 
        {
            Ken();
        }
    }
    public void Jump() 
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x,JumpSpeed);
        if(!currentState.Equals("Jump")) 
        {
            previousState = currentState;
        }
        SetCharacoterState("Jump");
    }
    public void Ken() 
    {

        if(!currentState.Equals("Ken")) {
            previousState = currentState;
        }
        SetCharacoterState("Ken");
    }

}
