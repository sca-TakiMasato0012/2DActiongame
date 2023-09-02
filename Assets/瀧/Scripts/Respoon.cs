using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respoon : MonoBehaviour
{
    public Transform RespoonPoint;

    private bool isRespooning = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DeathZone"))
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);

        transform.position = RespoonPoint.position;

        isRespooning = true;
    }
}
