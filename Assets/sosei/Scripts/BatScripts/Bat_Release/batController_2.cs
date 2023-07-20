using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class batController_2 : MonoBehaviour
{

    public GameObject target;//playerの取得

    Rigidbody2D rb;//コウモリの当たり判定

    [SerializeField]
    private float BatSpeed;
    private bool Speed = true;//コウモリの移動速度

    Bat_2SpineAnimationController bc;
    string default_animation = "kihon/huyuu";
    


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<Bat_2SpineAnimationController>();
        
    }



    // Update is called once per frame
    void Update()
    {
         // 左から右に移動
         transform.position += new Vector3(BatSpeed, 0, 0) * Time.deltaTime;
        
        Vector2 player = target.transform.position;

        float dis = Vector2.Distance(player, this.transform.position);
        if (dis < 8)
        {
           BatSpeed *=0;
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {//攻撃をくらったら

        if(collision.gameObject.tag == "ya") {

            BatSpeed *=0;
            StartCoroutine(BatMoveCooldown());
            bc.PlayAnimation(default_animation);
            //bf.PlayAnimation(orignal_animation);
            Debug.Log("yaがEffectにあたった!");
        }

    }

    IEnumerator BatMoveCooldown()//ひるむ
    {
        yield return new WaitForSeconds(0.5f);
        BatSpeed = -5.0f;
    }
}
