using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoburinController : MonoBehaviour
{
    private MeshRenderer sr = null;

    void Start()
    {
        sr = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(sr.isVisible)
        {
            Debug.Log("aioeagh");
        }
    }
}
