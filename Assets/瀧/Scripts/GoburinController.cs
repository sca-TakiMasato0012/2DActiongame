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

    private float dis;

    //private bool isAttack = false;

    [SerializeField]
    private string Stop_Animation = ""; //待機モーション
    [SerializeField]
    private string Start_Animation = ""; //歩くモーション
    [SerializeField]
    private string After_Animation = ""; //攻撃モーション

    [SerializeField]
    private float GoburinSpeed = 2.5f;//ゴブリンの移動速度

    private SkeletonAnimation skeletonAnimation = default;


    private Spine.AnimationState spineAnimationState = default;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;

        PlayAnimation(Stop_Animation); //最初は待機モーション
        isAnim = true;
    }

    bool isAnim = false;

    void Update()
    {
        dis = Vector3.Distance(transform.position, target.transform.position);

        Vector2 player = target.transform.position;

        isSearchArea = searchArea.IsSearching();

        if (!isAnim)
        {
            PlayAnimation(Start_Animation);//歩くアニメーションを再生
            isAnim = true;
        }
            

        if (isSearchArea) //Playerが範囲内に入ると歩き出す
        {

             float dis = Vector2.Distance(player, this.transform.position);
            if (dis < 1.5f)
            {
                //isAttack = true;
              

                Attack();
            }
             else
             {
                GoburinSpeed = 2.5f;

                //ここで歩きながら攻撃しちゃうから修正
             }

            transform.position += new Vector3(-GoburinSpeed, 0, 0) * Time.deltaTime;
            Debug.Log("はいってるよ");
        }

        if(!isSearchArea) //Playerが範囲内から抜けたら止まる
        {
            PlayAnimation(Stop_Animation);
            isAnim = false;
        }
        
    }

    private void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, name, true);
    }

    public void Attack()
    {
        dis = Vector3.Distance(transform.position, target.transform.position);

        GoburinSpeed = 0;

        if (dis < 2f)
        {
            PlayAnimation(After_Animation);//攻撃アニメーションを再生
            isAnim = true;
        }
        else
        {
            PlayAnimation(Stop_Animation);//待機アニメーションを再生
            isAnim = false;

            //PlayAnimation(After_Animation);//攻撃アニメーションを再生
            //isAnim = false;
        }
    }
}
