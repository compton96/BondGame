using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionCleverApproachItem : BTLeaf
{

    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2


    public CActionCleverApproachItem(string _name, CreatureAIContext _context) : base(_name, _context)
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
        agent.speed = context.CD.moveSpeed;
    }

    protected override void OnExit()
    {
        context.doMovement(0f);
        agent.ResetPath();
    }

    public override NodeState Evaluate()
    {
        agent.destination = context.cleverItem.transform.position;

        if (Vector3.Distance(context.cleverItem.transform.position, context.creatureTransform.position) < 3)
        {
            // Player too far away
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
