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
        {
            context.interactRadius.enabled = true;
            return NodeState.SUCCESS;
        }
            
        context.interactRadius.enabled = false;
        return NodeState.FAILURE;
    }


}
