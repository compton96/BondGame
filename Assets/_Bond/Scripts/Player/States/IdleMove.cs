using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerState
{
    [Serializable]
    public class IdleMove : State
    {
        public IdleMove( PlayerStateMachine _fsm ) : base( _fsm ) {
            parent = fsm.MovementState;
        }

        public override void OnStateEnter()
        {
            animator.OnIdle();
        }

        public override void OnStateUpdate()
        {
            
            if(player.playerDash)
            {
                SetState( fsm.Dash );
                return;
            }
            if(player.playerBasicAttack) 
            {
                //playerController.playerBasicAttack = false;
                SetState( fsm.ComboAttackState );
                return;
            }
            if(player.playerHeavyAttack) 
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
            
        }
    }
}