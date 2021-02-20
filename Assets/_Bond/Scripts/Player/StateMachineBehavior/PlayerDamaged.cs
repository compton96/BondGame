using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : PlayerSMB
{
    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBDamagedExit();
    }
}
