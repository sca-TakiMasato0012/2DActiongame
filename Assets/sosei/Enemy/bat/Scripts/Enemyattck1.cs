using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyattck1: MonoBehaviour
{

    public GameObject target;

    
        // Start is called before the first frame update
        void Start() 
        {
        
        }

    void Update() 
    {
        Vector3 stone = target.transform.position;
        float dis = Vector3.Distance(stone, this.transform.position);

        if(dis < 4.6f) 
        {
            Gravity();
        }

        void Gravity() 
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}