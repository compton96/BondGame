// Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionWildRunFromPlayer : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public CActionWildRunFromPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
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
        ranOnEnter = true;
    }

    protected override void OnExit()
    {
        ranOnEnter = false;
        context.doMovement(0f);
        agent.ResetPath();
        context.wildStartingLocation = context.creatureTransform.position;
    }

    public override NodeState Evaluate()
    {
        if(!ranOnEnter)
        {
            OnEnter();
        }
        //Debug.Log("Running from player");
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
            context.updateDebugText(name);
            return NodeState.RUNNING;
        }
    }
}