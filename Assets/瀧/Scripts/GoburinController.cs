using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class GoburinController : MonoBehaviour
{
    public SearchArea searchArea;

    public GameObject target;//playerの取得

    private bool isSearchArea = false;

    [SerializeField]
    private string Start_Animation = "";
    [SerializeField]
    private string After_Animation = "";

    private SkeletonAnimation skeletonAnimation = default;

    private Spine.AnimationState spineAnimationState = default;
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
    }

    bool isAnim = false;

    void Update()
    {
        Vector2 player = target.transform.position;

        isSearchArea = searchArea.IsSearching();

        if(isSearchArea&&!isAnim)
        {
            PlayAnimation(Start_Animation);//アニメーションを再生
            isAnim = true;

            Debug.Log("はいってるよ");
        }
    }

    private void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, name, true);
    }

}
