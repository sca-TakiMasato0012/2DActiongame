using Spine;
using Spine.Unity;
using UnityEngine;

/// <summary> Spineアニメーションの再生を確認サンプルクラス </summary>
public class SampleSpineAnimationController : MonoBehaviour
{

	/// <summary> 最初に再生するアニメーション名 </summary>
	[SerializeField]
	private string testAnimationNameBefore = "";

	/// <summary> 次に再生するアニメーション名 </summary>
	[SerializeField]
	private string testAnimationNameAfter = "";

	/// <summary> ゲームオブジェクトに設定されているSkeletonAnimation </summary>
	private SkeletonAnimation skeletonAnimation = default;

	/// <summary> Spineアニメーションを適用するために必要なAnimationState </summary>
	private Spine.AnimationState spineAnimationState = default;

	private void Start() {
		// ゲームオブジェクトのSkeletonAnimationを取得
		skeletonAnimation = GetComponent<SkeletonAnimation>();

		// SkeletonAnimationからAnimationStateを取得
		spineAnimationState = skeletonAnimation.AnimationState;
	}

	private void Update() {
		// Aキーの入力でアニメーションを切り替えるテスト
		if(Input.GetKeyDown(KeyCode.A)) {
			PlayAnimation();
		}
	}

	/// <summary>
	/// Spineアニメーションを再生
	/// testAnimationNameに再生したいアニメーション名を記載してください。
	/// </summary>
	private void PlayAnimation() {
		// アニメーション「testAnimationName」を再生
		TrackEntry trackEntry = spineAnimationState.SetAnimation(0, testAnimationNameBefore, true);

		// 完了通知を取得準備
		trackEntry.Complete += OnSpineComplete;
	}

	private void OnSpineComplete(TrackEntry trackEntry) {
		// アニメーション完了時に行う処理を記載
		spineAnimationState.SetAnimation(0, testAnimationNameAfter, true);
	}

}