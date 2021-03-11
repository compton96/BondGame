//Colin and Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class Slash1 : SlashBase
    {
        public Slash1( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash1";
            index = 1;
            hitBox = player.hitBoxes.slash1;

        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("slash1 enter");
        }
    }
}
