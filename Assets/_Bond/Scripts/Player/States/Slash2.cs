//Colin and Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class Slash2 : State
    {
        public PlayerAnimator playerAnimator => player.animator;

        public Slash2( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "Slash2";
            parent = fsm.ComboAttackState;
        }



        public override void OnStateEnter()
        {
            Debug.Log("Entering Slash2");
            player.playerBasicAttack = false;  
            playerAnimator.Attack2();            
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
                    //SetState(fsm.Slash);
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
            Debug.Log("Exiting Slash2");
            player.playerBasicAttack = false;
        }
    }
}
/*public class BasicHitState_2 : PlayerBaseState
{
    // bool punchAlternate = true;
    // public int noOfPresses = 0;
    // float lastPressTime = 0;
    // public float maxComboDelay;

    private PlayerController playerController;
    public PlayerAnimator playerAnimator => playerController.animator;

    public CapsuleCollider swordCollider; 


    public BasicHitState_2(PlayerController _playerController) : base(_playerController.gameObject) {
        playerController = _playerController;
    }


    public override void Enter(){
        //enter anim
           // playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            playerController.playerBasicAttack = false;
            
            playerController.swordCollider.enabled = true;
            playerAnimator.Attack2();
            //punchAlternate = !punchAlternate;

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
            //for going to 4th attack
            // playerController.isAnimDone = false;
            // swordCollider.enabled = false;
            // Debug.Log("Attack 2");
            // if(playerController.playerBasicAttack){
            //     playerController.playerBasicAttack = false;
            //     return typeof(BasicHitState_3);
            // }
            // playerAnimator.SetRun(false);
            // return typeof(PlayerIdleState);

            playerController.setIsAttackAnim(false);

            playerController.swordCollider.enabled = false;
            //Debug.Log("Attack 3");
            // Debug.Log("leaving");
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

    public void punch1AnimDone()
    {
        playerAnimator.attackDone();
    }

    public override void PhysicsTick()
    {
       playerController.doMovement(0.3f);
        playerController.doRotation(0.6f);
    }

    public override void Exit(){
        //exit anim
    }
}*/
