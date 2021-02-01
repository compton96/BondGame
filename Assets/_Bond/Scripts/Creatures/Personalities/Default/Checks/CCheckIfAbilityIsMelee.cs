using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCheckIfAbilityIsMelee : BTChecker
{
    public CCheckIfAbilityIsMelee(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if(context.lastTriggeredAbility >= 0)
        {
            Debug.Log("Checking if ability is melee");
            if(context.creatureStats.abilities[context.lastTriggeredAbility] is creatureAttackMelee)
            {
               Debug.Log("Ability is Melee");
                return NodeState.SUCCESS;
            }
        }
        
        return NodeState.FAILURE;
    }
}
