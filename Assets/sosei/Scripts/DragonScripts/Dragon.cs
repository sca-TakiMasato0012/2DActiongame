using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Dragon : MonoBehaviour
{

    [SerializeField]
    private float Dragon_Hp = 0;//ドラゴンのHp
    private SkeletonAnimation skeletonAnimation = default;

    private Spine.AnimationState spineAnimationState = default;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {




        if(Dragon_Hp <= 0)//もし倒されたら
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//攻撃をくらったら
    {
        if(collision.gameObject.tag == "ya") {
            //PlayAnimation(After_Animation);//ひるむアニメーションを再生

            Dragon_Hp = Dragon_Hp - 1.0f;//矢をくらったら１ダメージ

            
            Debug.Log("yaがEffectにあたった!");
        } 

    }
}
