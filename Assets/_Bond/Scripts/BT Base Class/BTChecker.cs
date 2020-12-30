using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTChecker : BTNode
{
    //bool toCheck
    protected CreatureAIContext context;

    public BTChecker(string _name, CreatureAIContext _context) : base(_name) {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
