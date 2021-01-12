using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EActionApproachPlayer : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public EActionApproachPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        agent = context.agent;
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
        agent.destination = enemyContext.player.transform.position;

        if(Vector3.Distance(enemyContext.player.transform.position, enemyContext.enemyTransform.position) > 20)
        {
            // Player too far away
            OnExit();
            return NodeState.FAILURE;
        } else if(enemyContext.isInPlayerRadius)
        {
            // Made it to player
            OnExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to player
            return NodeState.RUNNING;
        }
    }
}
