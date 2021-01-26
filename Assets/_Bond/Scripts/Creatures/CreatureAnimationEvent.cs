using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationEvent : MonoBehaviour
{
    private CreatureAnimator creatureAnimator => transform.parent.GetComponent<CreatureAnimator>();

    public void PlayWalkSFX()
    {
        creatureAnimator.PlayWalkSFX();
    }
}
