using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : MonoBehaviour
{

    public GameObject Rain;
    [SerializeField]
    public float qenerationInterval = 0.0f; //オブジェクトの生成間隔

    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= qenerationInterval)
        {
            Instantiate(Rain, transform.position, Quaternion.identity);

            timer = 0f;
        }
    }
}
