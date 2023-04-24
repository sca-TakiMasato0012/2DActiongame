using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rd2d;



    void Start()
    {
        this.rd2d = GetComponent<Rigidbody2D>();


    }

    void Update()
    {
        rd2d.AddForce(Vector2.right * speed);


    }
}
