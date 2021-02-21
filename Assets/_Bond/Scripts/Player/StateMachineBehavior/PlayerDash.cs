using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerSMB
{
    private bool isDashing;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isDashing = true;
    }

    public override void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isDashing = false;

        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBDashExit();
    }
}
