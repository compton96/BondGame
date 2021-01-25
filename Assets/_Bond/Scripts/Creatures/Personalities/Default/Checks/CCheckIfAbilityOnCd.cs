using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckIfAbilityOnCd: BTChecker
{
    public CCheckIfAbilityOnCd(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }
    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility >= 0)
        {
            if(context.cooldownSystem.IsOnCooldown(context.CD.abilities[context.lastTriggeredAbility].id))
            {
                Debug.Log("Ability on Cooldown");
                return NodeState.FAILURE;
            }
        }
        Debug.Log("Ability is free");
        context.cooldownSystem.PutOnCooldown(context.CD.abilities[context.lastTriggeredAbility]);
        return NodeState.SUCCESS;
    }
}
