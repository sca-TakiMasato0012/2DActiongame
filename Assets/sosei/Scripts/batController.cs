using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//�R�E�����̈ړ����x


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ������E�Ɉړ�
        transform.position += new Vector3(-BatSpeed, -1, 0) * Time.deltaTime;

        //��ʏ���ɏ�������e������
       if (transform.position.x <= -100.0f)
       {
           Destroy(gameObject);
       }
    }
}
