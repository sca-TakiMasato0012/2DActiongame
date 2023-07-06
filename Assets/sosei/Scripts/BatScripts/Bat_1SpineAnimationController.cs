using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Bat_1SpineAnimationController : MonoBehaviour
{
    public GameObject target;//playerの取得
    [SerializeField]
    private string testAnimationName = "";

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
            PlayAnimation();//アニメーションを再生
            isAnim = true;
        }

    }

    private void PlayAnimation() {

        spineAnimationState.SetAnimation(0, testAnimationName, true);
    }
}
