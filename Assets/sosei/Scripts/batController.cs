using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//�R�E�����̈ړ����x
    private float Angle;//�R�E�����̊p�x

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ������E�Ɉړ�
        transform.position += new Vector3(-BatSpeed, -Angle, 0) * Time.deltaTime;

        //��ʏ���ɏ�������e������
       if (transform.position.x <= -100.0f)
       {
           Destroy(gameObject);
       }
    }
}
