using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECheckIfDead : BTChecker
{
    public ECheckIfDead(string _name, EnemyAIContext _context) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        if (enemyContext.statManager.stats[ModiferType.CURR_HEALTH].modifiedValue < 1)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
