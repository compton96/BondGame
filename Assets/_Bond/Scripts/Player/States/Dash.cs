// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PlayerState
{
    [Serializable]
    public class Dash : State
    {
        public PlayerAnimator playerAnimator => player.animator;

        float startTime = 0;
        public Vector3 startRotation;

        // Set fields here
        public Dash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "Dash";
            parent = fsm.MovementState;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entering Dash");

            player.playerDash = false;
            playerAnimator.Dash();
            player.isDashing = true;

            startTime = Time.time;
            startRotation = player.v3Vel;
            player.setRotation(startRotation);
            
        }

        public override void OnStateUpdate()
        {
            if(Time.time > startTime + player.dashTime)
            {
                SetState(fsm.IdleMove);
            }
        }

        public override void OnStateFixedUpdate()
        {
            player.doMovement(player.dashSpeed);
            player.setRotation(startRotation);
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting Dash");
            player.isDashing = false;
        }
    }
}
/*public class PlayerDashState : PlayerBaseState
{
    float startTime = 0;
    private PlayerController playerController;
    public PlayerAnimator playerAnimator => playerController.animator;
    public Vector3 startRotation;
    


    public PlayerDashState(PlayerController _playerController) : base(_playerController.gameObject) {
        playerController = _playerController;
    }


    public override void Enter()
    {
        playerController.playerDash = false;
        playerAnimator.Dash();
        playerController.isDashing = true;
        startTime = Time.time;
        startRotation = playerController.v3Vel;
        playerController.setRotation(startRotation);
    }

   

    public override Type Tick() {
        
        //checks if time in this state has reached limit
        if(Time.time > startTime + playerController.dashTime)
        {
            playerController.isDashing = false;
            //playerAnimator.SetRun(false);
            return typeof(PlayerIdleState);
        }
       
        return null;
    }

    public override void PhysicsTick()
    {
        playerController.doMovement(playerController.dashSpeed);
        playerController.setRotation(startRotation);
       
    }

    public override void Exit(){
        //exit anim
    }
}*/
