using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionTrailPlayer : BTLeaf
{

    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2


    public CActionTrailPlayer(string _name, CreatureAIContext _context) : base(_name, _context) 
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
        //Debug.Log("Exiting Trail");
    }

    public override NodeState Evaluate()
    {
        // Quaternion desiredLook = context.player.transform.rotation;
        // context.doRotation(10f, desiredLook);
        // context.doMovement(context.CD.moveSpeed);

        agent.destination = context.followPoint.transform.position;

        if(Vector3.Distance(context.player.transform.position, context.creatureTransform.position) > 20)
        {
            // Player too far away
            OnParentExit();
            return NodeState.FAILURE;
        } else if(context.isInPlayerRadius)
        {
            // Made it to player
            OnParentExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to player
            context.updateDebugText(name);
            return NodeState.RUNNING;
        }
    }


}