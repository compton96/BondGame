using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerState
{
    public abstract class State
    {
        public PlayerStateMachine fsm { get; set; }
        public PlayerController player { get; set; }
        public PlayerAnimator animator => player.animator;

        protected string name;
        protected State parent;

        public State( PlayerStateMachine _fsm )
        {
            fsm = _fsm;
            player = fsm.player;
        }

        public virtual void OnStateEnter() { }
        public virtual void OnStateUpdate() { }
        public virtual void OnStateFixedUpdate() { }
        public virtual void OnStateExit() { }

        public void SetState( State state )
        {
            fsm.transitionExitState = this;

            fsm.currentState.OnParentStateExit();
            fsm.SetState( state );
        }

        // Used for default states to skip exit function
        public void SetDefaultState( State state )
        {
            fsm.SetState( state );
        }

        public void OnParentStateUpdate()
        {
            parent?.OnParentStateUpdate();
            OnStateUpdate();
        }
        public void OnParentStateFixedUpdate()
        {
            parent?.OnParentStateFixedUpdate();
            OnStateFixedUpdate();
        }
        private void OnParentStateExit()
        {
            OnStateExit();
            if ( !fsm.transitionExitState.Equals(this) )
            {
                parent?.OnParentStateExit();
            }
        }

    }
}