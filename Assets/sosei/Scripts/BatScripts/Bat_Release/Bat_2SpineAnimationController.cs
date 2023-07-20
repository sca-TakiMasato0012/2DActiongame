using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class Bat_2SpineAnimationController : MonoBehaviour
{

    public GameObject target;//playerの取得
    [SerializeField]
    private string default_Animation = "";
    [SerializeField]
    private string Start_Animation = "";//攻撃アニメーション
    [SerializeField]
    private string After_Animation = "";//ひるむアニメーション
    [SerializeField]
    private float Bat_Hp = 0;//コウモリのHP
    [SerializeField]
    private string Destroy_Animation = "";//死ぬアニメーション

    private SkeletonAnimation skeletonAnimation = default;//飛来するアニメーションを再生

    private Spine.AnimationState spineAnimationState = default;

  

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
    }

    bool isAnim= false;
    bool isAnim2 = false;

    // Update is called once per frame
    void Update()
    {
        
        Vector2 player = target.transform.position;

        float dis = Vector2.Distance(player, this.transform.position);//playerを見つけたら

        if(dis < 8 && !isAnim)
        {
            PlayAnimation(Start_Animation);//攻撃アニメーションを再生
            isAnim = true;

        } 

        if(Bat_Hp <= 0)//もし倒されたら
        {
            
            PlayAnimation(Destroy_Animation);//死ぬアニメーションを再生
            
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision) {//攻撃をくらったら
        if(collision.gameObject.tag == "ya") 
        {
            PlayAnimation(After_Animation);//ひるむアニメーションを再生

            Bat_Hp = Bat_Hp - 1.0f;//矢をくらったら１ダメージ

            Debug.Log("yaがSpineにあたった!");
        } 

    }
    public void PlayAnimation(string name) 
    {

        spineAnimationState.SetAnimation(0, Start_Animation, true);
    }


   
}