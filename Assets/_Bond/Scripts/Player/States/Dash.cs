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
        private float startTime = 0;
        private Vector3 startRotation;

        public Dash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Dash";
            parent = fsm.MovementState;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Dash Enter");
            player.inputs.dash = false;
            animator.Dash();
            player.isDashing = true;

            startTime = Time.time;
            // HERMAN TODO: Rename v3Vel
            startRotation = player.facingDirection;
            player.setRotation(startRotation);
        }

        public override void OnStateUpdate()
        {
            // HERMAN TODO: Decide if end of Dash should be animation or not
            if(Time.time > startTime + player.dashTime)
            {
                SetState(fsm.IdleMove);
                return;
            }
        }

        public override void OnStateFixedUpdate()
        {
            player.doMovement(player.dashSpeed);
            player.setRotation(startRotation);
        }

        public override void OnStateExit()
        {
            player.isDashing = false;
        }
    }
}
