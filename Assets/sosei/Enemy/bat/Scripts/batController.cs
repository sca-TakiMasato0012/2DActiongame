using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed;//�R�E�����̈ړ����x

    [SerializeField]
    private float Angle;//�R�E�����̊p�x
    public GameObject stone;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }


   
    // Update is called once per frame
    void Update()
    {
        // ���ォ��E���Ɉړ�
        transform.position += new Vector3(-BatSpeed, Angle, 0) * Time.deltaTime;

       //���Ɉړ�
        if (transform.position.x < -5.0f)
        {
            Angle = BatSpeed * 0;
        }

 
        //��ʏ���ɏ�������R�E����������
        if (transform.position.x <= -10.0f)
        {
            Destroy(gameObject);
         }
    }
}



