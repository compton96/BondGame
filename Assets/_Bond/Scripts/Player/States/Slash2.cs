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
    public class Slash2 : SlashBase
    {
        public Slash2( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash2";
            index = 2;
            hitBox = player.hitBoxes.slash0;
        }
    }
}
