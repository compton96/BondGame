//Jamo
// Herman for animations
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class HeavySlash : State
    {
        public HeavySlash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "HeavySlash";
            parent = fsm.InputState;
        }



        public override void OnStateEnter()
        {
            player.inputs.heavyAttack = false;
            player.heavyHitVfx.Play();
        }



        public override void OnStateUpdate()
        {
            //maybe do after anim ends
            SetState(fsm.IdleMove);
            return;
        }

        public override void OnStateFixedUpdate()
        {
            player.doMovement(0.1f);
            player.doRotation(0.1f);
        }

        public override void OnStateExit()
        {
            
        }
    }
}
