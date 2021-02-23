using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : PlayerSMB
{
    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBHurtExit();
    }
}
