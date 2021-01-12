using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChecker : BTNode
{
    //bool toCheck
    protected CreatureAIContext context;
    protected EnemyAIContext enemyContext;

    public BTChecker(string _name, CreatureAIContext _context) : base(_name) 
    {
        name = _name;
        context = _context;
    }

    public BTChecker(string _name, EnemyAIContext _context) : base(_name) 
    {
        name = _name;
        enemyContext = _context;
    }

    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
