using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECheckDamageTaken : BTChecker
{
    public ECheckDamageTaken(string _name, EnemyAIContext _context) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        if (enemyContext.tookDamage)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
