using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckEnthusiasmLow : BTChecker
{
    public CCheckEnthusiasmLow(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) <= 0)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
