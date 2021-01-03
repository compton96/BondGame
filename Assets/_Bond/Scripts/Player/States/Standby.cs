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
        // Set fields here
        public Standby( PlayerStateMachine _fsm ) : base( _fsm )
        {
            base.name = "Standby";
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entering Standby");
           
        }

        public override void OnStateUpdate()
        {
           
        }

        public override void OnStateFixedUpdate()
        {
          
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting Standby");
        }
    }
}

