using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckEnthusiasmInteracted : BTChecker
{
    public CCheckEnthusiasmInteracted(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.enthusiasmInteracted == false)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
