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
        int temp = context.player.GetComponent<PlayerController>().hasSwapped ? 100 : 0;
       
        if(context.player.GetComponent<PlayerController>().cooldownSystem.IsOnCooldown(10 + temp))
        {
          
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
