using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject Player;

    // プレイヤーの移動速度
    [SerializeField]
    private float PlayerSpeed;
    [SerializeField]
    private float JumpPower;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    //向き
    bool right = false;
    bool left = false;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("player");

        // 方向キーで入力された横軸の値を取得
        float x = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
        
        transform.position += new Vector3(x, 0, 0) * Time.deltaTime * PlayerSpeed;


        float y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)// スペースキーでジャンプ
        {
            rb.AddForce(new Vector2(0f, JumpPower), ForceMode2D.Impulse);
            isGrounded = false;
            transform.position += new Vector3(0, y, 0) * Time.deltaTime * JumpPower;
        }


        Vector3 scale = obj.transform.localScale;
        if (Input.GetKey(KeyCode.A) && left == false && scale.x > 0)
        {
            left = true;
            scale.x = -scale.x;
            obj.transform.localScale = scale;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }
        if (Input.GetKey(KeyCode.D) && right == false)
        {
            right = true;
            scale.x = 0.12f;
            obj.transform.localScale = scale;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")//地面についていたら
        {
            isGrounded = true;
        }
    }
}