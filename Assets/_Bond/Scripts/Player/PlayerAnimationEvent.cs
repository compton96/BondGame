using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   Written by Herman
*   This script acts as a "tunnel" to the playerAnimator component.
*   No actual code should go into this script.
*   Instead it should call a function in PlayerAnimator to help
*   consolidate the code into one script.
*/

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerAnimator playerAnimator => transform.parent.GetComponent<PlayerAnimator>();

    public void AttackDone()
    {
        playerAnimator.EventAttackDone();
    }

    // VISUAL FX

    public void PlaySlashVFX()
    {
        playerAnimator.PlaySlashVFX();
    }

    // SOUND FX
    
    public void PlayWalkSFX()
    {
        playerAnimator.PlayWalkSFX();
    }
}
