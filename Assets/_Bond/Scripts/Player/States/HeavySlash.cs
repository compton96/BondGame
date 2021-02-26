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

        public HeavySlash( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "HeavySlash";
            parent = fsm.InputState;
            hitbox = player.hitBoxes.heavy;
        }



        public override void OnStateEnter()
        {
            player.inputs.heavyAttack = false;

            animator.HeavyAttack();

            hitbox.SetActive(false);
            hitbox.SetActive(true);
        }



        public override void OnStateUpdate()
        {
            if(!animator.isHeavyAttack)
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
