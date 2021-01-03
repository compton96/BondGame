//Jamo + Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class Slash0 : State
    {
        public PlayerAnimator playerAnimator => player.animator;

        public Slash0( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "Slash0";
            parent = fsm.ComboAttackState;
        }



        public override void OnStateEnter()
        {
            Debug.Log("Entering Slash0");
            player.playerBasicAttack = false;  
            playerAnimator.Attack0();            
        }



        public override void OnStateUpdate()
        {
            if(player.playerDash)
            {
                playerAnimator.resetAllAttackAnims();                
                SetState(fsm.Dash);
            }

            if(!player.getIsAttackAnim())
            {
                player.setIsAttackAnim(false);

                if(player.playerBasicAttack)
                {
                    SetState(fsm.Slash1);
                }

                playerAnimator.SetRun(false);

                if(!player.getIsFollowThroughAnim())
                {
                    playerAnimator.resetAllAttackAnims();
                    SetState(fsm.IdleMove);
                }
            }
        }



        public override void OnStateFixedUpdate()
        {
            player.doMovement(0.3f);
            player.doRotation(0.6f);
        }



        public override void OnStateExit()
        {
            Debug.Log("Exiting Slash0");
            player.playerBasicAttack = false;
        }
    }
}
/*public class BasicHitState_0 : PlayerBaseState
{
    // bool punchAlternate = true;
    // public int noOfPresses = 0;
    // float lastPressTime = 0;
    // public float maxComboDelay;

    private PlayerController playerController;
    public PlayerAnimator playerAnimator => playerController.animator;

    public CapsuleCollider swordCollider; 


    public BasicHitState_0(PlayerController _playerController) : base(_playerController.gameObject) {
        playerController = _playerController;
    }


    public override void Enter(){
        //enter anim
        //Debug.Log("Enter State 0");
        playerController.playerBasicAttack = false;
        
        playerController.swordCollider.enabled = true;
        playerAnimator.Attack0();
    }

    public override Type Tick() {
        //Debug.Log("Attack State");
        if(playerController.playerDash)
        {
            playerAnimator.resetAllAttackAnims();
            playerController.swordCollider.enabled = false;
            return typeof(PlayerDashState);
        }
        if(!playerController.getIsAttackAnim())
        {
            
            playerController.setIsAttackAnim(false);

            playerController.swordCollider.enabled = false;

            if(playerController.playerBasicAttack){
                playerController.playerBasicAttack = false;
                return typeof(BasicHitState_1);
            }
            playerAnimator.SetRun(false);

            if(!playerController.getIsFollowThroughAnim()){
                playerAnimator.resetAllAttackAnims();
                return typeof(PlayerIdleState);
            }
        } 
        //disable movement?
        //trigger attack animation
        
        //apply hitbox on area
        //
        //on animation done enable movement/
        
        //do idle anim
        return null;
    }


    public override void PhysicsTick()
    {
        playerController.doMovement(0.3f);
        playerController.doRotation(0.6f);
    }

    public override void Exit(){
        //exit anim
    }
}
*/