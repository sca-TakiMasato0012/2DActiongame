using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{ 
    [SerializeField]
    private float CountTimer= 0f; // カウントダウン開始秒数をセット

    public static float CountDownTime; // カウントダウンタイム
    

    public Text TextCountDown; // 表示用テキストUI


    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = CountTimer;    
    }
    
    // Update is called once per frame
    void Update()
    {
        // カウントダウンタイムを整形して表示
        TextCountDown.text = String.Format("残り時間:{0:00秒}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
    }
}
