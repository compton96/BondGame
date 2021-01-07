//Colin and Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    //NOT BEING USED, UNTIL 4TH ATTACK ANIM MADE
    [Serializable]
    public class Slash3 : SlashBase
    {
        // Set fields here
        public Slash3( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash3";
            index = 3;
            hitBox = player.hitBoxes.slash0;
        }
    }
}
