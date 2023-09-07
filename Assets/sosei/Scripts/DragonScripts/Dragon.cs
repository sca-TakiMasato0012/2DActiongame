using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class Dragon : MonoBehaviour
{

    public GameObject target;//playerの取得

    [SerializeField]
    private string default_Animation = "";
    [SerializeField]
    private string Start_Animation = "";//攻撃アニメーション
    [SerializeField]
    private float Dragon_Hp = 0;//ドラゴンのHp
    private SkeletonAnimation skeletonAnimation = default;

    private Spine.AnimationState spineAnimationState = default;

    Rigidbody2D rb;
    [SerializeField]
    private float DragonSpeed;


    private Transform player; // プレイヤーキャラクターのTransformコンポーネント
    private bool facingLeft = false; // キャラクターが右を向いているかどうか

    bool isAnim = false;
    

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

        // プレイヤーのX座標とキャラクターのX座標を比較して、振り向く
        if (player != null)
        {
            if (transform.position.x < player.position.x && !facingLeft)
            {
                // プレイヤーが右にいる場合、キャラクターを右に向ける
                Flip();
            }
            else if (transform.position.x > player.position.x && facingLeft)
            {
                // プレイヤーが左にいる場合、キャラクターを左に向ける
                Flip();
            }
        }

        transform.position += new Vector3(DragonSpeed, 0, 0) * Time.deltaTime;

        float dis = Vector2.Distance(player1, this.transform.position);

        if (dis < 8 && !isAnim)
        {
            PlayAnimation(Start_Animation);//攻撃アニメーションを再生
            isAnim = true;
            DragonSpeed = 0;
        }
        if (Dragon_Hp <= 0)//もし倒されたら
        {
            Destroy(gameObject);
        }
    
}
    private void OnCollisionEnter2D(Collision2D collision)//攻撃をくらったら
    {
        if (collision.gameObject.tag == "ya")
        {
            //PlayAnimation(After_Animation);//ひるむアニメーションを再生

            Dragon_Hp = Dragon_Hp - 1.0f;//矢をくらったら１ダメージ


            Debug.Log("yaがEffectにあたった!");
        }

    }
    public void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, Start_Animation, true);
    }
    private void Flip()
    {
        // キャラクターのスケールを反転させて、向きを変更する
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        
    }
}