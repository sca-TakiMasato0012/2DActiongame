using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyattck1: MonoBehaviour
{

    public GameObject target;//playerの取得
    Rigidbody2D rb;
    [SerializeField]
    private float Gravity; //石の落下スピード
    

    // Start is called before the first frame update
    void Start() 
        {
            rb = GetComponent<Rigidbody2D>();
        }

    void Update() 
    {
        Vector2 player = target.transform.position;
        float dis = Vector2.Distance(player,this.transform.position);//stone, this.transform.position

        if (dis < 5) 
        {
            Vector2 myGravity = new Vector2(0,-Gravity);

            rb.AddForce(myGravity);//Gravity();
        }



        if(transform.position.y < -7.0f) 
        {
            Destroy(gameObject);
        }

        // void Gravity() 
        {
            //GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player") {

            Destroy(gameObject);
        }
    }
    
}