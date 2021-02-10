//Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckAbilityOne : BTChecker
{
    public CCheckAbilityOne(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }
    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility == 0)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
