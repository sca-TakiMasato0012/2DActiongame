using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// キャラクターのモーション遷移を管理（Spine）
/// </summary>
public class CharactorMotionState : StateMachineBehaviour
{
    public string _animationName;
    public bool _loop = false;
    public float _timeScale = 1f;
    SkeletonAnimation _spineAnim;
    Animator _animator;
    bool _instate = false;
    bool _init = false;


    /// <summary>
    /// アニメーション遷移開始とともに、初期設定を行う
    /// </summary>
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _instate = true;
        if (!_init)
        {
            _spineAnim = animator.GetComponent<SkeletonAnimation>();
            _animator = animator;
            _spineAnim.state.Complete += OnComplete;

            _init = true;
        }
        _spineAnim.timeScale = _timeScale;
        _spineAnim.state.SetAnimation(0, _animationName, _loop);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _instate = false;
    }

    /// <summary>
    /// Spineの動作が終了したときの設定
    /// </summary>
    void OnComplete(Spine.TrackEntry entry)
    {
        if (_instate)
        {
            // Spineアニメーションが終了したとき、Animator Controllerのトリガーをセットする
            if (_loop == false)
            {
                _animator.SetTrigger("AnimationEnd");
            }
            else
            {
                _animator.ResetTrigger("AnimationEnd");
            }
        }
    }
}