using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class GoburinController : MonoBehaviour
{
    public SearchArea searchArea; //サーチ範囲

    Rigidbody2D rb;//ゴブリンの当たり判定

    public GameObject target;//playerの取得

    private bool isSearchArea = false;

    [SerializeField]
    private string Stop_Animation = "";
    [SerializeField]
    private string Start_Animation = "";
    [SerializeField]
    private string After_Animation = "";

    [SerializeField]
    private float GoburinSpeed = 2.5f;//ゴブリンの移動速度

    private SkeletonAnimation skeletonAnimation = default;

    private Spine.AnimationState spineAnimationState = default;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
    }

    bool isAnim = false;

    void Update()
    {

        Vector2 player = target.transform.position;

        isSearchArea = searchArea.IsSearching();

        if (!isAnim)
        {
            PlayAnimation(Start_Animation);//アニメーションを再生
            isAnim = true;
        }
            

        if (isSearchArea)
        {

             float dis = Vector2.Distance(player, this.transform.position);
            if (dis < 1.5f)
            {
                GoburinSpeed = 0;

            }
             else
             {
                GoburinSpeed = 2.5f;
             }



            transform.position += new Vector3(-GoburinSpeed, 0, 0) * Time.deltaTime;
            Debug.Log("はいってるよ");
        }
        

    }

    private void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, name, true);
    }

}
