using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerState
{
    [Serializable]
    public class InputState : State
    {
        public InputState( PlayerStateMachine _fsm ) : base( _fsm ) {}

        public override void OnStateEnter()
        {
            SetDefaultState( fsm.MovementState );
        }

        public override void OnStateUpdate()
        {
            if(player.isHit)
            {
                player.isHit = false;

                SetState( fsm.Damaged );
                return;
            }
        }

        public override void OnStateFixedUpdate()
        {
            
        }

        public override void OnStateExit()
        {
            
        }
    }
}