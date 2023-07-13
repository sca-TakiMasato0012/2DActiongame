using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDef : MonoBehaviour
{
    Rigidbody2D rb;
    private float Arrow_destroy = 5.0f;
  

    [SerializeField]
    private float Arrowspeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, Arrow_destroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * -Arrowspeed * Time.deltaTime);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") {

            Destroy(gameObject);
        }
        Debug.Log("あたった");
    }
}
