using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerSMB
{
    override public void OnStateEnter(Animator unityAnimator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnimator animator = GetPlayerAnimator( unityAnimator );

        animator.SMBIdleEnter();
    }
}
