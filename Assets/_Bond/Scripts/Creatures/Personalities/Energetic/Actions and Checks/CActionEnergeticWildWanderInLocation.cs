// Eugene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CActionEnergeticWildWanderInLocation : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 9f;
    private float angularSpeed = 960f; //deg/s
    private float acceleration = 150f; //max accel units/sec^2

    public CActionEnergeticWildWanderInLocation (string _name, CreatureAIContext _context) : base(_name, _context) 
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
        context.wanderDestination = context.wildStartingLocation;
        //this is just to make sure the creature doesn't walk too short of a distance
        //might have to rework this in the future but eh, works now
        while (Vector3.Distance(context.wanderDestination, context.creatureTransform.position) < 8f) 
        {
            context.wanderDestination = context.wildStartingLocation + (Random.insideUnitSphere * context.wanderRadius);
            //this stuff *might* run a bit slow, so we might have to edit this later
            //but basically finds the closest point on the navmesh to the random location
            NavMeshHit hit;
            if (NavMesh.SamplePosition(context.wanderDestination, out hit, context.wanderRadius, NavMesh.AllAreas))
            {
                context.wanderDestination = hit.position;
            }
        }
        //Debug.Log(Vector3.Distance(context.wanderDestination, context.creatureTransform.position));
    }

    protected override void OnExit()
    {
        ranOnEnter = false;
        context.doMovement(0f);
        agent.ResetPath();
        context.wanderIdling = true;
    }

    public override NodeState Evaluate()
    {
        //if idling, don't run the rest of the function
        if (context.wanderIdling)
        {
            return NodeState.FAILURE;
        }

        if(!ranOnEnter)
        {
            OnEnter();
        }

        //agent.destination = context.player.transform.position
        agent.destination = context.wanderDestination;

        if(Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= 2f)
        {
            // creature got to wander destination
            OnExit();
            return NodeState.SUCCESS;
        }
        else
        {
            // still wandering
            return NodeState.RUNNING;
        }
    }
}