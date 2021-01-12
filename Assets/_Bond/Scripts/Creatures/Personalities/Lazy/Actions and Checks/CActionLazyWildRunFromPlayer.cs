// Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionLazyWildRunFromPlayer : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 2f;
    private float angularSpeed = 500f; //deg/s
    private float acceleration = 50f; //max accel units/sec^2

    public CActionLazyWildRunFromPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
        agent = context.agent;
        //ObjToFollow = captCreature.followPoint;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.speed = moveSpeed;
    }

    protected override void OnEnter()
    {
        
    }

    protected override void OnExit()
    {
        context.doMovement(0f);
        agent.ResetPath();
        context.wildStartingLocation = context.creatureTransform.position;
    }

    public override NodeState Evaluate()
    {
        //Vector3 desiredLook = new Vector3(context.player.transform.position.x, context.creatureTransform.transform.position.y, context.player.transform.position.z);
        //context.doLookAt(desiredLook);
        //context.doMovement(context.CD.moveSpeed);
        //agent.destination = context.backFollowPoint.transform.position;
        Vector3 position_difference = context.creatureTransform.position - context.player.transform.position;
        position_difference.Normalize();
        agent.destination = context.creatureTransform.position + position_difference * 10;

        if(Vector3.Distance(context.player.transform.position, context.creatureTransform.position) > 10)
        {
            // creature escaped player
            OnExit();
            return NodeState.SUCCESS;
        }
        else if( context.player.GetComponent<PlayerController>().currSpeed <= context.playerSpeedToScare )
        {
            return NodeState.FAILURE;
        }
        else 
        {
            // Still trying to get to player
            return NodeState.RUNNING;
        }
    }
}