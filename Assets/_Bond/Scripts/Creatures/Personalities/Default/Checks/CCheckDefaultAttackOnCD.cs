using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckDefaultAttackOnCD : BTChecker
{
    public CCheckDefaultAttackOnCD(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility >= 0)
        {
            if(context.player.GetComponent<PlayerController>().cooldownSystem.IsOnCooldown(10))
            {
                context.isAbilityTriggered = false;
                return NodeState.FAILURE;
            }
        }
        return NodeState.SUCCESS;
    }
}
