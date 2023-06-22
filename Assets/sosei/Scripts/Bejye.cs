using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bejye : MonoBehaviour
{
    public Transform sunrise;
    public Transform sunset;
    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
      
        // The center of the arc
        Vector3 center = (sunrise.position + sunset.position) * speed;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, -1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;

        
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
    
    
    
}
