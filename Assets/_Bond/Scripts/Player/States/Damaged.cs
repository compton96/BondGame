// Jake
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
            base.name = "Damaged";
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entering Damaged");
            player.animator.animator.SetTrigger("isHit");
            
        }

        public override void OnStateUpdate()
        {
            if(player.isHit)
            {
                player.isHit = false;
                SetState( fsm.Damaged);
            }
            //wait till end of animation, return to idle
            //maybe check how many times has been triggered in succession?
            if(!player.animator.animator.GetCurrentAnimatorStateInfo(0).IsTag("isHit"))
            {
                //Debug.Log("End Player hit state");
                
                SetState(fsm.IdleMove);
            }
        }

        public override void OnStateFixedUpdate()
        {
          
        }

        public override void OnStateExit()
        {
            Debug.Log("Exiting Damaged");
        }
    }
}
/*public class Damaged : PlayerBaseState
{
    private PlayerController player;
    
    public PlayerIsHit(PlayerController _player) : base(_player.gameObject){
        player = _player;
    }

    public override void Enter(){
        //enter anim
        player.animator.animator.SetTrigger("isHit");
    }

    public override Type Tick() {
        if(player.isHit)
        {
            player.isHit = false;
            return typeof(PlayerIsHit);
        }
        //wait till end of animation, return to idle
        //maybe check how many times has been triggered in succession?
        if(!player.animator.animator.GetCurrentAnimatorStateInfo(0).IsTag("isHit"))
        {
            //Debug.Log("End Player hit state");
            
            return typeof(PlayerIdleState);
        }
        return null;
    }


    public override void PhysicsTick()
    {
        
    }

    public override void Exit(){
        //exit anim
       // Debug.Log("Exiting Player isHit");
    }
}
*/