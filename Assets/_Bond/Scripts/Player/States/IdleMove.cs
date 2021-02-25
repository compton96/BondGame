using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerState
{
    [Serializable]
    public class IdleMove : State
    {
        public IdleMove( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "IdleMove";
            parent = fsm.MovementState;
        }

        public override void OnStateEnter()
        {
            Debug.Log("idle enter");
            animator.Idle( true );
        }

        public override void OnStateUpdate()
        {
            if(player.inputs.dash)
            {
                SetState( fsm.Dash );
                return;
            }
            if(player.inputs.basicAttack) 
            {
                //playerController.playerBasicAttack = false;
                SetState( fsm.ComboAttackState );
                return;
            }
            if(player.inputs.heavyAttack) 
            {
                //playerController.playerBasicAttack = false;
                SetState( fsm.HeavyCharge );
                return;
            }
            
        }

        public override void OnStateFixedUpdate()
        {
            player.doMovement(1f);
            player.doRotation(1f);
        }

        public override void OnStateExit()
        {
            animator.Idle( false );
        }
    }
}