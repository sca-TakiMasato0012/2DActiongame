using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{

    
    Rigidbody2D rb;//コウモリの当たり判定

    
   private float destroyTime = 4.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        


        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,destroyTime);
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
 
    }
    
}



