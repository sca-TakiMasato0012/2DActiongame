using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//�R�E�����̈ړ����x
    [SerializeField]
    private float Angle;//�R�E�����̊p�x

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ������E�Ɉړ�
        transform.position += new Vector3(-BatSpeed, Angle, 0) * Time.deltaTime;

        //��ʏ���ɏ�������R�E����������
       if (transform.position.x <= -10.0f)
       {
           Destroy(gameObject);
       }
    }

    Vector3 CalcLerpPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }



    public void StartThrow(GameObject Bat, float height, Vector3 start, Vector3 end, float duration)
    {
        // ���_�����߂�
        Vector3 half = end - start * 0.50f + start;
        half.y += Vector3.up.y + height;

        StartCoroutine(LerpThrow(Bat, start, half, end, duration));
    }


    IEnumerator LerpThrow(GameObject Bat, Vector3 start, Vector3 half, Vector3 end, float duration)
    {
        float startTime = Time.timeSinceLevelLoad;
        float rate = 0f;
        while (true)
        {
            if (rate >= 1.0f)
                yield break;

            float diff = Time.timeSinceLevelLoad - startTime;
            rate = diff / (duration / 60f);
            Bat.transform.position = CalcLerpPoint(start, half, end, rate);

            yield return null;
        }
    }

}
