// Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionLazyApproachPlayer : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 1f;
    private float angularSpeed = 500f; //deg/s
    private float acceleration = 50f; //max accel units/sec^2

    public CActionLazyApproachPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
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
    }

    public override NodeState Evaluate()
    {
        //Vector3 desiredLook = new Vector3(context.player.transform.position.x, context.creatureTransform.transform.position.y, context.player.transform.position.z);
        //context.doLookAt(desiredLook);
        //context.doMovement(context.CD.moveSpeed);
        agent.destination = context.player.transform.position;

        if(!context.isNoticed)
        {
            // Player too far away
            OnExit();
            return NodeState.FAILURE;
        } 
        else if(Vector3.Distance(context.creatureTransform.position, context.player.transform.position) < 2f)
        {
            // Made it to player
            OnExit();
            return NodeState.SUCCESS;
        }
        else
        {
            // Still trying to get to player
            return NodeState.RUNNING;
        }
    }
}