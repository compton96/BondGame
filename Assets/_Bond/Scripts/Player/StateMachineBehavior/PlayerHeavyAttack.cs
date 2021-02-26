using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttack : PlayerSMB
{
    override public void OnStateExit(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBHeavyAttackExit();
    }
}
