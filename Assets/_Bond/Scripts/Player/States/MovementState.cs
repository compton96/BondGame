using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerState
{
    [Serializable]
    public class MovementState : State
    {
        public MovementState( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Movement";
            parent = fsm.InputState;
        }

        public override void OnStateEnter()
        {
            SetDefaultState( fsm.IdleMove );
        }

        public override void OnStateUpdate()
        {
            
        }

        public override void OnStateFixedUpdate()
        {
            
        }

        public override void OnStateExit()
        {
            
        }
    }
}