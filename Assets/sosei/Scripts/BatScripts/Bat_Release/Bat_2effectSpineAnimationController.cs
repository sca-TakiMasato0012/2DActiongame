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

    bool isAnim = false;

    private Transform player; // プレイヤーキャラクターのTransformコンポーネント
    private bool facingLeft = false; // キャラクターが右を向いているかどうか

    private SkeletonAnimation skeletonAnimation = default;//飛来するアニメーション

    private Spine.AnimationState spineAnimationState = default;
    // Start is called before the first frame update
    void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    
    // Update is called once per frame
    void Update() {
       

        Vector2 player1 = target.transform.position;

        if (player != null)
        {
            if (transform.position.x < player.position.x && !facingLeft)
            {
                // プレイヤーが右にいる場合、キャラクターを右に向ける
                FlipAndRotate(0);
            }
            else if (transform.position.x > player.position.x && facingLeft)
            {
                // プレイヤーが左にいる場合、キャラクターを左に向ける
                FlipAndRotate(0);
            }
        }
        float dis = Vector2.Distance(player1, this.transform.position);//playerを見つけたら
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


    private void FlipAndRotate(float rotationAngle)
    {
        // キャラクターのスケールを反転させて、向きを変更する
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // キャラクターを指定したY軸の回転角度だけ回転させる
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.y -= 180;
        transform.eulerAngles= eulerAngles;
        // キャラクターを指定したZ軸の回転角度だけ回転させる
        eulerAngles.z *= -1;
        transform.eulerAngles = eulerAngles;
    }
}
