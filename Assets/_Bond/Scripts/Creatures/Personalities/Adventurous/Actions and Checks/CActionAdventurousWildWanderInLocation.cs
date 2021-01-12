// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionAdventurousWildWanderInLocation : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 3.5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public CActionAdventurousWildWanderInLocation(string _name, CreatureAIContext _context) : base(_name, _context) 
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
        context.wanderDestination = context.wildStartingLocation;
        //this is just to make sure the creature doesn't walk too short of a distance
        //might have to rework this in the future but eh, works now
        while (Vector3.Distance(context.wanderDestination, context.creatureTransform.position) < 6f) 
        {
            context.wanderDestination = context.wildStartingLocation + (Random.insideUnitSphere * (context.wanderRadius * 2));
            //this stuff *might* run a bit slow, so we might have to edit this later
            //but basically finds the closest point on the navmesh to the random location
            NavMeshHit hit;
            if (NavMesh.SamplePosition(context.wanderDestination, out hit, context.wanderRadius * 2, NavMesh.AllAreas))
            {
                context.wanderDestination = hit.position;
            }
        }
        Debug.Log(Vector3.Distance(context.wanderDestination, context.creatureTransform.position));
    }

    protected override void OnExit()
    {
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

        //agent.destination = context.player.transform.position
        agent.destination = context.wanderDestination;

        if(Vector3.Distance(context.wanderDestination, context.creatureTransform.position) <= 2f)
        {
            // creature got to wander destination
            OnExit();
            return NodeState.SUCCESS;
        } else 
        {
            // still wandering
            return NodeState.RUNNING;
        }
    }
}