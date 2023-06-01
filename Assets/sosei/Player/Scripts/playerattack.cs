using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("AddForce1");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-10, -10, 0) * Time.deltaTime;
    }
    /*IEnumerator AddForce1() 
    {
        
        transform.position += new Vector3(10, 100, 0) * Time.deltaTime;

        if(transform.position.x > 10) {

            yield return new WaitForSeconds(1);
        }

        
        transform.position += new Vector3(10, 10, 0) * Time.deltaTime;

        //StartCoroutine("AddForce2");
    }
    IEnumerator AddForce2() 
    {

        //青色にする
        transform.position += new Vector3(-10, 10, 0) * Time.deltaTime;

        //1秒停止
        yield return new WaitForSeconds(1);

        //黄色にする
        transform.position += new Vector3(-10, -10, 0) * Time.deltaTime;

    }*/
}
