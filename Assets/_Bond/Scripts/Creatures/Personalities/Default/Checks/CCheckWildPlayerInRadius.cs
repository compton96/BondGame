//Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckWildPlayerInRadius : BTChecker
{

    public CCheckWildPlayerInRadius(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.isNoticed)
            return NodeState.SUCCESS;
        return NodeState.FAILURE;
    }
}
