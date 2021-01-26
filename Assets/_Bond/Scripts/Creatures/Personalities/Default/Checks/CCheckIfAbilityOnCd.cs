// Eugene and ????????
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
            if(context.player.GetComponent<PlayerController>().cooldownSystem.IsOnCooldown(context.creatureStats.abilities[context.lastTriggeredAbility].id))
            {
                context.isAbilityTriggered = false;
                Debug.Log("Ability on Cooldown");
                return NodeState.FAILURE;
            }
        }
        Debug.Log("Ability is free");
        return NodeState.SUCCESS;
    }
}
