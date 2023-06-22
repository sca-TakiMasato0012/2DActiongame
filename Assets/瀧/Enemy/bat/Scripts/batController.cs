using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{

    public GameObject target;//playerの取得

    Rigidbody2D rb;//コウモリの当たり判定

    [SerializeField]
    private float BatSpeed;//コウモリの移動速度

   
    public GameObject stone;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


   
    // Update is called once per frame
    void Update()
    {
       /* // 左上から右下に移動
        transform.position += new Vector3(-BatSpeed, Angle, 0) * Time.deltaTime;

       //横に移動
        if (transform.position.x < 6.0f)
        {
            Angle = BatSpeed * 0;
        }
       */
 
        //画面上方に消えたらコウモリを消去
        if (transform.position.x <= -15.0f)
        {
            Destroy(gameObject);
        }
    }
}



