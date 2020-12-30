using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe rename this class?
public class CCheckDistanceToTarget : BTChecker
{
    public CCheckDistanceToTarget(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(context.creatureTransform.position, context.targetEnemy.transform.position);
        Debug.Log("Distance to Target : " + distance);
        if(context.CD.abilities[context.lastTriggeredAbility] is creatureAttackMelee)
        {
            creatureAttackMelee attack = (creatureAttackMelee) context.CD.abilities[context.lastTriggeredAbility];
            Debug.Log("Ability is melee, now checking distance : " + attack.maxDistanceToEnemy);
            if (distance < attack.maxDistanceToEnemy) return NodeState.FAILURE; 
        } else if(context.CD.abilities[context.lastTriggeredAbility] is creatureAttackRanged) 
        {
            creatureAttackRanged attack = (creatureAttackRanged) context.CD.abilities[context.lastTriggeredAbility];
            if (distance < attack.maxDistanceToEnemy) return NodeState.FAILURE; 
        }
        
        return NodeState.SUCCESS;
        
    }
}
