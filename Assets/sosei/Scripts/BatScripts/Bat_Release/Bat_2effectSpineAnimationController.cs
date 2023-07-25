using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Bat_2effectSpineAnimationController : MonoBehaviour
{


    public GameObject target;//playerの取得

    [SerializeField]
    private string orignal_Animation = "";
    [SerializeField]
    private string Start_Animation = "";//Effect2のアニメーション
    [SerializeField]
    private string After_Animation = "";//ひるむアニメーション


    private SkeletonAnimation skeletonAnimation = default;//飛来するアニメーション

    private Spine.AnimationState spineAnimationState = default;
    // Start is called before the first frame update
    void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
    }

    bool isAnim = false;
    
    // Update is called once per frame
    void Update() {
       

        Vector2 player = target.transform.position;

        float dis = Vector2.Distance(player, this.transform.position);//playerを見つけたら
        if(dis < 8 && !isAnim) 
        {
            PlayAnimation(Start_Animation);//Effect2のアニメーションを再生
            isAnim = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) //攻撃をくらったら
    {
    
        if(collision.gameObject.tag == "ya") 
        {

            //PlayAnimation(After_Animation);//ひるむアニメーションを再生

        Debug.Log("yaがEffectにあたった!");
        }
        else
        {
            PlayAnimation(orignal_Animation);
        }
        
    }



    public void PlayAnimation(string name) {

        spineAnimationState.SetAnimation(0, Start_Animation, true);
    }


    
}
