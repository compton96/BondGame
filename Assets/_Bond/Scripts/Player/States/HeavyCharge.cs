//Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class HeavyCharge : State
    {

        
        public PlayerAnimator playerAnimator => player.animator;
        
        ParticleSystem vfx;

        // Set fields here
        public HeavyCharge( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "HeavyCharge";
            parent = fsm.InputState;
        }



        public override void OnStateEnter()
        {
            Debug.Log("Entering HeavyCharge");
            player.heavyChargeVfx.Play();
        }



        public override void OnStateUpdate()
        {
            if(player.playerHeavyAttack == false) SetState(fsm.HeavySlash);
        }



        public override void OnStateFixedUpdate()
        {
            player.doMovement(0f);
            player.doRotation(0f);
        }



        public override void OnStateExit()
        {
            Debug.Log("Exiting HeavyCharge");
            player.heavyChargeVfx.Stop();
        }
    }
}
/*public class HeavyChargeState : PlayerBaseState
{
    private PlayerController playerController;
    public PlayerAnimator playerAnimator => playerController.animator;
    public CapsuleCollider swordCollider; 
    ParticleSystem vfx;

    


    public HeavyChargeState(PlayerController _playerController) : base(_playerController.gameObject) {
        playerController = _playerController;
    }


    public override void Enter(){
      
        //soon to be deprecated
        
        playerController.swordCollider.enabled = true;
        playerController.heavyChargeVfx.Play();

        //trigger charge animation

        //Debug.Log("heavy charge state");
        //vfx = gameObject.GetComponentInChildren<ParticleSystem>();
            

    }

    public override Type Tick() {
       
        //check for input action to be cancelled,
        //action.canceled += ctx =>  Action was canceled ; 
        //if so then
        if(playerController.playerHeavyAttack == false) return typeof(HeavyHitState);
        
        //make ui rnage indicator grow over time, or just trigger it idk
        
        return null;
    }

    public override void PhysicsTick()
    {
        playerController.doMovement(0f);
        playerController.doRotation(0f);
    }

    public override void Exit(){
        //exit anim
        //reset
        playerController.heavyChargeVfx.Stop();
    }
}*/
