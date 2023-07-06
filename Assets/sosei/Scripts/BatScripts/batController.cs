using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{

    public GameObject target;//player‚Ìæ“¾
    Rigidbody2D rb;//ƒRƒEƒ‚ƒŠ‚Ì“–‚½‚è”»’è
  

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
        /* // ¶ã‚©‚ç‰E‰º‚ÉˆÚ“®
         transform.position += new Vector3(-BatSpeed, Angle, 0) * Time.deltaTime;

        //‰¡‚ÉˆÚ“®
         if (transform.position.x < 6.0f)
         {
             Angle = BatSpeed * 0;
         }
        */

    }

}



