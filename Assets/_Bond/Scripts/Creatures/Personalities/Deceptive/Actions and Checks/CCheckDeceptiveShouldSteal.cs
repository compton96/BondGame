// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckDeceptiveShouldSteal : BTChecker
{

    public CCheckDeceptiveShouldSteal(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        context.stealTimer += Time.deltaTime;
        if (context.stealTimer >= context.stealDuration) 
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }

}
