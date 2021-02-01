﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerAnimator playerAnimator => transform.parent.GetComponent<PlayerAnimator>();

    public void AttackDone()
    {
        playerAnimator.AttackDone();
    }

    public void DamagedDone()
    {
        playerAnimator.DamagedDone();
    }

    public void FollowThroughDone()
    {
        playerAnimator.FollowThroughDone();
    }

    public void PlaySlashVFX()
    {
        playerAnimator.PlaySlashVFX();
    }

    public void PlayWalkSFX()
    {
        playerAnimator.PlayWalkSFX();
    }
}
