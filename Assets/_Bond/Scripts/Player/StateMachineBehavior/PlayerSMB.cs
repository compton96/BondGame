using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSMB : StateMachineBehaviour
{
    public PlayerAnimator GetPlayerAnimator(Animator animator)
    {
        return animator.gameObject.transform.parent.GetComponent<PlayerAnimator>();
    }
}
