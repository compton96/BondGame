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
    *   Should be formatted "isX" like a question
    *
    *   Public constants Can be read by other scripts
    *   But can only be set in here
    *   
    */
    public bool isAttack { get; private set; }
    public bool isAttackFollowThrough { get; private set; }
    public bool isDash { get; private set; }
    public bool isHurt { get; private set; }

    private int attackStatesActive = 0;

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

    public void SMBAttackEnter()
    {
        attackStatesActive += 1;
    }

    public void SMBAttackExit()
    {
        attackStatesActive -= 1;
        if( attackStatesActive < 1 )
        {
            isAttack = false;
            isAttackFollowThrough = false;
        }
    }

    public void SMBDashExit()
    {
        isDash = false;
        animator.ResetTrigger("Dash");
    }

    public void SMBHurtExit()
    {
        isHurt = false;
        animator.ResetTrigger("isHit");
    }

    /*
    *   Actual Functions
    *   Modifies the constants
    *   Called by the states
    */

    public void Attack( int num )
    {
        isAttack = true;
        isAttackFollowThrough = true;

        animator.SetTrigger("Attack" + num.ToString() );
    }

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

    public void Crouch(bool state)
    {
        // Herman TODO: Make this value lerp
        if( state )
        {
            animator.SetFloat("Standing_Crouch_Blend", 1f );
        }
        else
        {
            animator.SetFloat("Standing_Crouch_Blend", 0f );
        }
    }

    public void Dash( float constant )
    {
        isDash = true;
        animator.SetTrigger("Dash");
        animator.SetFloat("DashConstant", 1/constant);

        this.ResetAllAttackAnims();
    }

    public void Hurt()
    {
        isHurt = true;
        Run( false );
        animator.SetTrigger("isHit");
    }

    public void Idle()
    {
        this.ResetAllAttackAnims();

        animator.ResetTrigger("Dash");
        animator.ResetTrigger("isHit");
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

    public void Run(bool state)
    {
        animator.SetBool("Run", state);
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
