using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_pd_Controller : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // プレイヤーがジャンプするときに追加される力の量
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;  // 動きをどれだけ滑らかにするか
    [SerializeField] private bool m_AirControl = false;                         // プレイヤーがジャンプ中に操縦できるかどうか。
    [SerializeField] private LayerMask m_WhatIsGround;                          // キャラクターの基礎となるものを決定する
    [SerializeField] private Transform m_GroundCheck;                           // プレーヤーが接地しているかどうかを確認する位置を示すマーク。
    [SerializeField] private Transform m_WallCheck;								// あなたの人格をコントロールする姿勢を保ちます

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
