using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   Written by Herman
*/

public class PlayerAnimator : MonoBehaviour
{
    public GameObject model;
    private Animator animator => model.GetComponent<Animator>();
    private PlayerController playerController => GetComponent<PlayerController>();

    /*
    *   Constants
    *   Can be read by other scripts
    *   But can only be set in here
    */
    public bool isAttack { get; private set; }
    public bool isDamaged { get; private set; }
    public bool isFollowThrough { get; private set; }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    */

    public void EventAttackDone()
    {
        isAttack = false;
    }

    public void EventDamagedDone()
    {
        isDamaged = false;
        animator.ResetTrigger("isHit");
    }

    public void EventFollowThroughDone()
    {
        isAttack = false;
        isFollowThrough = false;
    }

    /*
    *   State Machine Behavior Triggers
    *   Triggered by State Machine Behaviors
    */

    public void SMBAttackDone()
    {
        //isAttack = false;
        //isFollowThrough = false;
    }

    public void SMBDamagedExit()
    {
        isDamaged = false;
        animator.ResetTrigger("isHit");
    }

    public void SMBIdleEnter()
    {
        isAttack = false;
        isFollowThrough = false;
    }

    /*
    *   Reset Functions
    *   Modifies the constants
    */

    public void ResetAttackAnim()
    {
        isAttack = false;
        isFollowThrough = false;
    }

    public void ResetAllAttackAnims()
    {
        isAttack = false;
        isFollowThrough = false;

        animator.ResetTrigger("Attack0");
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack4");
    }

    public void Attack( int num )
    {
        isAttack = true;
        isFollowThrough = true;

        animator.SetTrigger("Attack" + num.ToString() );
    }

    public void SetDamaged()
    {
        isDamaged = true;
        animator.SetTrigger("isHit");
    }

    public void Dash()
    {
        animator.SetTrigger("Dash");

        this.ResetAllAttackAnims();
    }

    public void SetRun(bool state)
    {
        animator.SetBool("Run", state);
    }

    public void Move(Vector3 movementVector)
    {
        if (movementVector.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void OnIdle()
    {
        this.ResetAllAttackAnims();

        animator.ResetTrigger("Dash");
    }

    // VISUAL FX

    public void PlaySlashVFX()
    {
        playerController.slashVfx.Play();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Swing", transform.position);
    }

    // SOUND FX

    public void PlayWalkSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Walking Grass 2D", transform.position);
    }
}
