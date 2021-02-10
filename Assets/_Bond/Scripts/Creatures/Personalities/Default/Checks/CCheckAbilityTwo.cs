// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckAbilityTwo : BTChecker
{
    public CCheckAbilityTwo(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }
    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility == 1)
        {
            return NodeState.SUCCESS;
        } else 
        {
            return NodeState.FAILURE;
        }
    }
}
