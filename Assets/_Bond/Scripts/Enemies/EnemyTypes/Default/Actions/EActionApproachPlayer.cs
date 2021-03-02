using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EActionApproachPlayer : BTLeaf
{
    private NavMeshAgent agent;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public EActionApproachPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        agent = enemyContext.agent;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        agent.speed = enemyContext.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
    }

    protected override void OnEnter()
    {
        agent.speed = enemyContext.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
        //Play awake anim
    }

    protected override void OnExit()
    {
        agent.ResetPath();
    }

    public override NodeState Evaluate() 
    {
        if(Vector3.Distance(enemyContext.player.transform.position, enemyContext.enemyTransform.position) > 
                enemyContext.statManager.stats[ModiferType.DETECTION_RANGE].modifiedValue)
        {
            // Player too far away
            OnParentExit();
            return NodeState.FAILURE;
        } else if(enemyContext.isInPlayerRadius || enemyContext.animator.inAttack)
        {
            // Made it to player
            OnParentExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to player
            agent.destination = enemyContext.player.transform.position;
            agent.speed = enemyContext.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
            return NodeState.RUNNING;
        }
    }
}
