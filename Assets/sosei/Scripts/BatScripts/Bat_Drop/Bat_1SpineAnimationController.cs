using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Bat_1SpineAnimationController : MonoBehaviour
{
    public GameObject target;//playerの取得

    public GameObject stone;//stoneの取得

    [SerializeField]
    private string Start_Animation = "";
    [SerializeField]
    private string After_Animation = "";
    private SkeletonAnimation skeletonAnimation = default;

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
        if(dis < 5 && !isAnim) {
            PlayAnimation(Start_Animation);//アニメーションを再生
            isAnim = true;
        }

        if(dis < 4 && isAnim) //もし石が落ちたら
        { 
         PlayAnimation(After_Animation);//hirai/kakuuアニメーションを再生 

        }

    }

    private void PlayAnimation(string name) 
    {

        spineAnimationState.SetAnimation(0,name, true);
    }

   
}
