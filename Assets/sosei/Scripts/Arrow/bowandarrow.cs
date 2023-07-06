using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowandarrow : MonoBehaviour
{

    public Transform arrowstartcheck;
    public Transform endcheck;

    // Time to move from sunrise to sunset position, in seconds.
    public float DoTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // The center of the arc
        Vector3 center = (arrowstartcheck.position + endcheck.position) * 0.5F;

        // move the center a bit downwards to make the arc vertical
        center -= new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = arrowstartcheck.position - center;
        Vector3 setRelCenter = endcheck.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / DoTime;

        transform.position = Vector3.Lerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}
