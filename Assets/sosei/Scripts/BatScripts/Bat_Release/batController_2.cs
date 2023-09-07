using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;


public class batController_2 : MonoBehaviour
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

    private Transform player; // プレイヤーキャラクターのTransformコンポーネント
    private bool facingLeft = false; // キャラクターが右を向いているかどうか
    bool isAnim = false;
    bool isAnim2 = false;
    
    

    Rigidbody2D rb;//コウモリの当たり判定

    [SerializeField]
    private float BatSpeed;
    private bool Speed = true;//コウモリの移動速度

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
        // プレイヤーキャラクターのTransformコンポーネントを取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   

    // Update is called once per frame
    void Update()
    {
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
        // 左から右に移動
        transform.position += new Vector3(BatSpeed, 0, 0) * Time.deltaTime;
        
        

        float dis = Vector2.Distance(player1, this.transform.position);

        //playerを見つけたら

        if (dis < 8 && !isAnim)
        {
            PlayAnimation(Start_Animation);//攻撃アニメーションを再生
            BatSpeed = 0;
            isAnim = true;

        }

        if (Bat_Hp <= 0)//もし倒されたら
        {

            PlayAnimation(Destroy_Animation);//死ぬアニメーションを再生
            StartCoroutine(BatDestroy());
            Destroy(gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)//攻撃をくらったら
    {
        if (collision.gameObject.tag == "ya")
        {
            //PlayAnimation(After_Animation);//ひるむアニメーションを再生

            Bat_Hp = Bat_Hp - 1.0f;//矢をくらったら１ダメージ

            BatSpeed = 0;
            StartCoroutine(BatMoveCooldown());
            Debug.Log("yaがEffectにあたった!");
        }
        else
        {
            PlayAnimation(default_Animation);

        }
        
    }

    IEnumerator BatMoveCooldown()//ひるむ
    {
        yield return new WaitForSeconds(0.5f);

        BatSpeed = -5.0f;
        
    }

    IEnumerator BatDestroy()//死亡
    {
        yield return new WaitForSeconds(1.5f);
        
    }

    public void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, Start_Animation, true);
    }

  
    private void FlipAndRotate(float rotationAngle)
    {
        // キャラクターのスケールを反転させて、向きを変更する
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        // キャラクターを指定したZ軸の回転角度だけ回転させる
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z *=-1;
        transform.eulerAngles = eulerAngles;
    }
}
