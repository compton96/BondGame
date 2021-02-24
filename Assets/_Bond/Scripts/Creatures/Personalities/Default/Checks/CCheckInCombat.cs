using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckInCombat : BTChecker
{
    public CCheckInCombat(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.inCombat)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
