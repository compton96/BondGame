//Colin and Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{

    //NOT BEING USED, UNTIL 4TH ATTACK ANIM MADE
    [Serializable]
    public class Slash3 : State
    {
        // Set fields here
        public Slash3( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "Slash3";
            parent = fsm.ComboAttackState;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entering Slash3");
            //SetDefaultState( fsm.Test3 );
        }

        public override void OnStateUpdate()
        {
            if(player.playerBasicAttack)
            {
                player.playerBasicAttack = false;
                //SetState(fsm.Test4);
            }
        }

        public override void OnStateFixedUpdate()
        {
          
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting Slash3");
        }
    }
}
/*public class BasicHitState_3 : PlayerBaseState
{
    // bool punchAlternate = true;
    // public int noOfPresses = 0;
    // float lastPressTime = 0;
    // public float maxComboDelay;

    private PlayerController playerController;
    public PlayerAnimator playerAnimator => playerController.animator;

    public CapsuleCollider swordCollider; 


    public BasicHitState_3(PlayerController _playerController) : base(_playerController.gameObject) {
        playerController = _playerController;
    }


    public override void Enter(){
        //enter anim
            // playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            playerController.playerBasicAttack = false;
            swordCollider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<CapsuleCollider>();
            swordCollider.enabled = true;
            playerAnimator.Attack2();
            //punchAlternate = !punchAlternate;

    }

    public override Type Tick() {
        //Debug.Log("Attack State");
        if(playerController.playerDash)
        {
            playerAnimator.resetAttackAnim();
            swordCollider.enabled = false;
            return typeof(PlayerDashState);
        }
        if(!playerController.getIsAttackAnim())
        {
            
            playerAnimator.resetAttackAnim();

            swordCollider.enabled = false;

            playerAnimator.SetRun(false);
            return typeof(PlayerIdleState);
        } 
        //disable movement?
        //trigger attack animation
        
        //apply hitbox on area
        //
        //on animation done enable movement/
        
        //do idle anim
        return null;
    }

    public void punch1AnimDone()
    {
        playerAnimator.attackDone();
    }

    public override void PhysicsTick()
    {
        playerController.doMovement(0.1f);
        playerController.doRotation(0.1f);
    }

    public override void Exit(){
        //exit anim
    }
}*/
