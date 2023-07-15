using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Bejye : MonoBehaviour
{
    // Time to move from sunrise to sunset position, in seconds.
    [SerializeField]
    private float speed;
    private float Arrow_destroy = 5.0f;

    //public bool isMoving = false;
    //private Coroutine movecorutine;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Arrow_destroy);
    }
    // Update is called once per frame
    void Update()
    {
        

       transform.position += new Vector3(1,1,0) * Time.deltaTime;

    }


}