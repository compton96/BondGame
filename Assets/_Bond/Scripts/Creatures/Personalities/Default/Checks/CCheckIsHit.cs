using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckIsHit : BTChecker
{
    public CCheckIsHit(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.isHit)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
