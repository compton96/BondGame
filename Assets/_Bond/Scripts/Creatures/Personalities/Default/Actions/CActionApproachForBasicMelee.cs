using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionApproachForBasicMelee : BTLeaf
{
    private NavMeshAgent agent;
    creatureAttackBase attack;
    private float moveSpeed = 15f;
    private float maxDist;

    public CActionApproachForBasicMelee(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
        agent = context.agent;
    }

    protected override void OnEnter()
    {
        creatureAttackMelee _attack = (creatureAttackMelee) context.basicCreatureAttack;
        maxDist = _attack.maxDistanceToEnemy;

        agent.speed = moveSpeed;
        // agent.stoppingDistance = attack.maxDistanceToEnemy;
    }

    protected override void OnExit()
    {
        agent.speed = context.creatureStats.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
        agent.stoppingDistance = 1f;
        agent.ResetPath();
    }

    public override NodeState Evaluate() {
        // Debug.Log("APROACH FOR ATTACK");
        agent.destination = context.targetEnemy.transform.position;
        float distance = Vector3.Distance(context.creatureTransform.position, context.targetEnemy.transform.position);
        // Debug.Log("Approaching Enemy : " + distance+ " MAX dist: " + maxDist);
        // Debug.Log("Distance : " + distance + " maxDist : " + attack.maxDistanceToEnemy);
        if(distance < maxDist)
        {
            // Made it to Enemy
            OnParentExit();
            return NodeState.SUCCESS;
        } else
        {
            // Still trying to get to Enemy
            context.updateDebugText(name);
            return NodeState.RUNNING;
        }
    }
}
