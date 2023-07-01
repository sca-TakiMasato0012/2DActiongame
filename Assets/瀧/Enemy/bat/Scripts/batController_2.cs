using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController_2 : MonoBehaviour
{

    public GameObject target;//playerの取得

    Rigidbody2D rb;//コウモリの当たり判定

    [SerializeField]
    private float BatSpeed;//コウモリの移動速度

    


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
       
    }



    // Update is called once per frame
    void Update()
    {
         // 左上から右下に移動
         transform.position += new Vector3(BatSpeed, 0, 0) * Time.deltaTime;

        Vector2 player = target.transform.position;
        float dis = Vector2.Distance(player, this.transform.position);//stone, this.transform.position
        if (dis < 8)
        {
            BatSpeed *=0;
        }

       
        
    }

}
