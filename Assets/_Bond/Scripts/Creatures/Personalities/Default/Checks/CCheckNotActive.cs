using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckNotActive : BTChecker
{
    public CCheckNotActive(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.isActive == false)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
