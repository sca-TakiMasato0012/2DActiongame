using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bejye : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetPoint(Vector3 p0,Vector3 p1,Vector3 p2,Vector3 p3, float t) 
    {
        var a = Vector3.Lerp(p0,p1,t);
        var b = Vector3.Lerp(p1, p2, t);
        var c = Vector3.Lerp(p2, p3, t);
        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d,e,t);

    }
}
