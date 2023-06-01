using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//コウモリの移動速度

    [SerializeField]
    private float Angle;//コウモリの角度


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        // 左から右に移動
        transform.position += new Vector3(-BatSpeed, Angle, 0) * Time.deltaTime;

        if(transform.position.y <= 1) 
        {
            Angle =BatSpeed *  0;
        }

            //画面上方に消えたらコウモリを消去
            if (transform.position.x <= -10.0f)
       {
           Destroy(gameObject);
       }
    }
}
