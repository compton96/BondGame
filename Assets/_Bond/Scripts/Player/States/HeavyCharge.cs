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
    public class HeavyCharge : State
    {
        private ParticleSystem vfx;

        public HeavyCharge( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "HeavyCharge";
            parent = fsm.InputState;
        }


        // HERMAN TODO: place VFX code into animator
        public override void OnStateEnter()
        {
            player.heavyChargeVfx.Play();
        }



        public override void OnStateUpdate()
        {
            if(player.inputs.dash)
            {
                SetState( fsm.Dash );
                return;
            }
            
            if(player.inputs.heavyAttack == false)
            {
                SetState(fsm.HeavySlash);
                return;
            }
        }



        public override void OnStateFixedUpdate()
        {
            player.doMovement(0f);
            player.doRotation(0f);
        }



        public override void OnStateExit()
        {
            player.heavyChargeVfx.Stop();
        }
    }
}
