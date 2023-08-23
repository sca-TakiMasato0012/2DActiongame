using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_con : MonoBehaviour
{
    [SerializeField]
    private float P_moveSpeed= 0f;

    [SerializeField]
    private string taiki_ken_Animation = "";
    [SerializeField]
    private string taiki_yumi_Animation = "";
    [SerializeField]
    private string run_Animation = "";
    [SerializeField]
    private string dash_Animation = "";

    private SkeletonAnimation skeletonAnimation = default;

    private Spine.AnimationState spineAnimationState = default;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * P_moveSpeed * Time.deltaTime;
        transform.Translate(movement);




       

    }
    public void PlayAnimation(string name)
    {

        spineAnimationState.SetAnimation(0, taiki_ken_Animation, true);
    }
}
