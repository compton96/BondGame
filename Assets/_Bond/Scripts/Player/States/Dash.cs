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
        private Vector3 startRotation;
        private CharacterController controller;

        public Dash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Dash";
            parent = fsm.MovementState;
        }

        public override void OnStateEnter()
        {
            player.inputs.dash = false;
            player.isDashing = true;
            animator.Dash( stats.getStat(ModiferType.DASH_DURATION) );
            
            startRotation = player.facingDirection;
            player.setRotation(startRotation);
            controller = player.gameObject.GetComponent<CharacterController>();

            player.lastMoveVec = player.inputs.moveDirection; 

            //temp fix for hitbox 2 coming on after dash?????????????????
            
        }

        public override void OnStateUpdate()
        {
            if( !animator.isDash )
            {
                SetState(fsm.IdleMove);
                return;
            }
        }

        public override void OnStateFixedUpdate()
        {
            
            player.doMovement(stats.getStat(ModiferType.DASH_SPEED));
            player.setRotation(startRotation);
           //controller.Move(movementVector);
           //controller.Move(gravity * Time.deltaTime);
        }

        public override void OnStateExit()
        {
            player.isDashing = false;
        }
    }
}
