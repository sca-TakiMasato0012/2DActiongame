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

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // 方向キーで入力された横軸の値を取得
        float x = Input.GetAxis("Horizontal");
        transform.position += new Vector3(x, 0, 0) * Time.deltaTime * PlayerSpeed;// 現在位置にxの値を加算する
  
        float y = Input.GetAxis("Vertical");
        // スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, JumpPower), ForceMode2D.Impulse);
            isGrounded = false;
            transform.position += new Vector3(0, y, 0) * Time.deltaTime * JumpPower;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}