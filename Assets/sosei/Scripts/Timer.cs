using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{ 
    [SerializeField]
    private float CountTimer= 0f; // �J�E���g�_�E���J�n�b�����Z�b�g

    public static float CountDownTime; // �J�E���g�_�E���^�C��
    

    public Text TextCountDown; // �\���p�e�L�X�gUI


    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = CountTimer;    
    }
    
    // Update is called once per frame
    void Update()
    {
        // �J�E���g�_�E���^�C���𐮌`���ĕ\��
        TextCountDown.text = String.Format("�c�莞��:{0:00�b}", CountDownTime);
        // �o�ߎ����������Ă���
        CountDownTime -= Time.deltaTime;
    }
}
