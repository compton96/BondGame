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
        public GameObject hitbox;
        public float startTime;
        public float endLag = 0.6f;
        public HeavySlash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "HeavySlash";
            parent = fsm.InputState;
            hitbox = player.hitBoxes.heavy;
        }



        public override void OnStateEnter()
        {
            player.inputs.heavyAttack = false;

            // HERMAN TODO: Put animator code here
            player.heavyHitVfx.Play();

            hitbox.SetActive(false);
            hitbox.SetActive(true);
            startTime = Time.time;

        }



        public override void OnStateUpdate()
        {
            //maybe do after anim ends
            //make animation longer, has a recovery period.
            if(Time.time > startTime + endLag)
            {
                SetState(fsm.IdleMove);
                return;
            }
            
        }

        public override void OnStateFixedUpdate()
        {
            //player.doMovement(0.1f);
            //player.doRotation(0.1f);
        }

        public override void OnStateExit()
        {
    
        }
    }
}
