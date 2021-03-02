// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace PlayerState
{
    [Serializable]
    public class Hurt : State
    {
        // Set fields here
        public Hurt( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Hurt";
        }

        public override void OnStateEnter()
        {
            
            player.isHit = false;
            animator.Hurt();
        }

        public override void OnStateUpdate()
        {
            if(player.isHit)
            {
        
                player.isHit = false;
                SetState(fsm.Hurt);
                return;
            }
            //wait till end of animation, return to idle
            if( !animator.isHurt )
            {
                SetState(fsm.IdleMove);
                return;
            }
            Debug.Log(animator.isHurt);
        }

        public override void OnStateFixedUpdate()
        {
          
        }

        public override void OnStateExit()
        {
        
        }
    }
}
