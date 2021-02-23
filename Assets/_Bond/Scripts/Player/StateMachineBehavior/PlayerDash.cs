using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerSMB
{
    public override void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBDashExit();
    }
}
