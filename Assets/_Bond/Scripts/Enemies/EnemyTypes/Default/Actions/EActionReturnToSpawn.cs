// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EActionReturnToSpawn : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public EActionReturnToSpawn(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        agent = enemyContext.agent;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        //agent.speed = moveSpeed;
    }

    protected override void OnEnter()
    {
        agent.speed = enemyContext.activeEnemyData.moveSpeed;
        //Play awake anim
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        Debug.Log("Returning to spawn");
        agent.destination = enemyContext.startingLocation;

        if(Vector3.Distance(enemyContext.enemyTransform.position, enemyContext.startingLocation) < 10)
        {
            // Made it to spawn
            OnExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to spawn
            return NodeState.RUNNING;
        }
    }
}
