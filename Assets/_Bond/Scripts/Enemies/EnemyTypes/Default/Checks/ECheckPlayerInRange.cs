using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECheckPlayerInRange : BTChecker
{
    public ECheckPlayerInRange(string _name, EnemyAIContext _context) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        if (enemyContext.playerNoticed)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
