using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{

    [SerializeField] GameObject Bat;


    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(stop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator stop()
    {
       
        yield return new WaitForSeconds(5f);

    }
}
