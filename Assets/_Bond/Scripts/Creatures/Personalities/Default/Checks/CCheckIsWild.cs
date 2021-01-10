using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckIsWild : BTChecker
{

    public CCheckIsWild(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.isWild)
            return NodeState.SUCCESS;
        return NodeState.FAILURE;
    }


}
