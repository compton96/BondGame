//Jamo + Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class SlashBase : State
    {
        protected int index;
        protected State nextState;
        protected GameObject hitBox;

        public SlashBase( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash Base";
            parent = fsm.ComboAttackState;
            
        }



        private State GetNextState()
        {
            switch(index)
            {
                case 0:
                    return fsm.Slash1;
                case 1:
                    return fsm.Slash2;
                case 2:
                    return null; //fsm.Slash3;
                default:
                    return null;
            }
        }



        public override void OnStateEnter()
        {
            player.inputs.basicAttack = false;  
            nextState = GetNextState();
            
            animator.Attack( index );
        }



        public override void OnStateUpdate()
        {
            if(player.inputs.dash)
            {
                SetState( fsm.Dash );
                return;
            }
            
            if(!animator.isAttack)
            {
                if( nextState != null )
                {
                    if(player.inputs.basicAttack)
                    {
                        SetState( nextState );
                        return;
                    }
                }
                
                animator.SetRun(false);

                if(!animator.isFollowThrough)
                {
                    animator.ResetAllAttackAnims();

                    SetState(fsm.IdleMove);
                    return;
                }
            }
        }



        public override void OnStateFixedUpdate()
        {
            player.doMovement(0.3f);
            player.doRotation(0.6f);
        }



        public override void OnStateExit()
        {
            player.inputs.basicAttack = false;
        }
    }
}
