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

        if(transform.position.y <= 1) 
        {
            Angle =BatSpeed *  0;
        }

            //��ʏ���ɏ�������R�E����������
            if (transform.position.x <= -10.0f)
       {
           Destroy(gameObject);
       }
    }
}
