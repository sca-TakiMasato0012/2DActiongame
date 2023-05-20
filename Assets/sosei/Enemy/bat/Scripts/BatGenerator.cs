using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGenerator : MonoBehaviour
{
    public GameObject BatPrefab;
    [SerializeField]
    public int appearancerate; //èoåªó¶
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, appearancerate)==1)
        {
            Vector3 pos = new Vector3(10,Random.Range(10.0f,-5.0f), 0);
            Instantiate(BatPrefab, pos, Quaternion.identity);
        }
    }
}