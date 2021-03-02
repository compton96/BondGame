using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckPlayerTriggeredAbility : BTChecker
{
    public CCheckPlayerTriggeredAbility(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }



    public override NodeState Evaluate()
    {
        if(context.isAbilityTriggered)
        {
            // Debug.Log("Player Triggered Ability");
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
