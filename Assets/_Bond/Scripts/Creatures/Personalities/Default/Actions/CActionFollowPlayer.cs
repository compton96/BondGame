// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionFollowPlayer : BTLeaf
{

    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2


    public CActionFollowPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;

        agent = context.agent;
        //ObjToFollow = captCreature.followPoint;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        //agent.speed = moveSpeed;
    }

    protected override void OnEnter()
    {
        ranOnEnter = true;
        agent.speed = context.CD.moveSpeed;
    }

    protected override void OnExit()
    {
        ranOnEnter = false;
        context.doMovement(0f);
        agent.ResetPath();
    }

    public override NodeState Evaluate()
    {
        if(!ranOnEnter)
        {
            OnEnter();
        }
        //Vector3 desiredLook = new Vector3(context.player.transform.position.x, context.creatureTransform.transform.position.y, context.player.transform.position.z);
        //context.doLookAt(desiredLook);
        //context.doMovement(context.CD.moveSpeed);
        agent.destination = context.backFollowPoint.transform.position;

        if(Vector3.Distance(context.player.transform.position, context.creatureTransform.position) > 20)
        {
            // Player too far away
            OnExit();
            return NodeState.FAILURE;
        } else if(context.isInPlayerRadius || context.isInPlayerTrail)
        {
            // Made it to player
            OnExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to player
            context.updateDebugText(name);
            return NodeState.RUNNING;
        }
    }
}


/*
shoot action logic?

move until you have raycast then shoot

or
while raycast between navmesh parts returns false

    move to stopping range
    decrease stopping range

shoot
*/
