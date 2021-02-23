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
    *   Should be formatted "isX" like a question
    */
    public bool isAttack { get; private set; }
    public bool isDamaged { get; private set; }
    public bool isDash { get; private set; }
    public bool isAttackFollowThrough { get; private set; }

    /*
    *   Animation Events
    *   Triggered in PlayerAnimationEvent.CS
    */

    public void EventAttackDone()
    {
        isAttack = false;
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

    public void SMBDashExit()
    {
        isDash = false;
        animator.ResetTrigger("Dash");
    }

    public void SMBIdleEnter()
    {
        isAttack = false;
        isAttackFollowThrough = false;
    }

    /*
    *   Reset Functions
    *   Modifies the constants
    */

    public void ResetAttackAnim()
    {
        isAttack = false;
        isAttackFollowThrough = false;
    }

    public void ResetAllAttackAnims()
    {
        isAttack = false;
        isAttackFollowThrough = false;

        animator.ResetTrigger("Attack0");
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack4");
    }

    public void Attack( int num )
    {
        isAttack = true;
        isAttackFollowThrough = true;

        animator.SetTrigger("Attack" + num.ToString() );
    }

    public void Damaged()
    {
        isDamaged = true;
        Run( false );
        animator.SetTrigger("isHit");
    }

    public void Dash( float constant )
    {
        isDash = true;
        animator.SetTrigger("Dash");
        animator.SetFloat("DashConstant", 1/constant);

        this.ResetAllAttackAnims();
    }

    public void HeavyCharge(bool state)
    {
        if( state )
        {
            playerController.heavyChargeVfx.Play();
        }
        else
        {
            playerController.heavyChargeVfx.Stop();
        }
    }

    public void Run(bool state)
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

    public void Idle()
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
