﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PlayerState
{
    [Serializable]
    public class Damaged : State
    {
        // Set fields here
        public Damaged( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Damaged";
        }

        public override void OnStateEnter()
        {
            animator.SetDamaged();
        }

        public override void OnStateUpdate()
        {
            if(player.isHit)
            {
                player.isHit = false;
                SetState( fsm.Damaged );
                return;
            }
            //wait till end of animation, return to idle
            if( !player.animator.isDamaged )
            {
                SetState(fsm.IdleMove);
                return;
            }
        }

        public override void OnStateFixedUpdate()
        {
          
        }

        public override void OnStateExit()
        {
            
        }
    }
}
