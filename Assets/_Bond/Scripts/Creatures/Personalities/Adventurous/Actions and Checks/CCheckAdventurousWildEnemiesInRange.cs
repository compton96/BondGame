//Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CCheckAdventurousWildEnemiesInRange : BTChecker
{

    public CCheckAdventurousWildEnemiesInRange(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        int layermask = 1 << 8; //only layer 8 will be targeted
        Collider[] hitColliders = Physics.OverlapSphere(context.creatureTransform.position, context.enemyDetectRange * .75f, layermask);
        foreach (var hitCollider in hitColliders)
        { 
            Debug.Log("FOUND ENEMY");
            context.targetEnemy = hitCollider.gameObject;
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
