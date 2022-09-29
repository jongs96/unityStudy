using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyIK : MonoBehaviour
{
    public Animator myAnim;
    public Transform myLeftHandTarget;
    public float myWeight = 1.0f;

    public Transform myLeftFoot;
    public Transform myRightFoot;
    public LayerMask FootMask = default;
    public bool IsObscale = false;
    Vector3 leftFootPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        IsObscale = false;
        if (Physics.Raycast(myLeftFoot.position + Vector3.up * 1.0f, Vector3.down, out RaycastHit hit, 1.5f, FootMask))
        {
            leftFootPos = hit.point;
            IsObscale = true;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (myLeftHandTarget != null)
        {
            myAnim.SetIKPositionWeight(AvatarIKGoal.LeftHand, myWeight);
            myAnim.SetIKPosition(AvatarIKGoal.LeftHand, myLeftHandTarget.position);
            myAnim.SetIKRotationWeight(AvatarIKGoal.LeftHand, myWeight);
            myAnim.SetIKRotation(AvatarIKGoal.LeftHand, myLeftHandTarget.rotation);
        }

        if (IsObscale)
        {
            myAnim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
            myAnim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
        }
    }
}
