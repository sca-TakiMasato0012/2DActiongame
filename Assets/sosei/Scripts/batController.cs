using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//コウモリの移動速度


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 左から右に移動
        transform.position += new Vector3(-BatSpeed, 0, 0) * Time.deltaTime;

        // 画面上方に消えたら弾を消去
        //if (transform.position.x >= -100.0f)
       // {
         //   Destroy(gameObject);
       // }
    }
}
