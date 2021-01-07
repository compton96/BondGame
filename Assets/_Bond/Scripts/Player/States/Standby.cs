using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class Standby : State
    {
        public Standby( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Standby";
        }

        public override void OnStateEnter()
        {
            
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

