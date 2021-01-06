//Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckScrawnyWildPlayerMoving : BTChecker
{
    public CCheckScrawnyWildPlayerMoving(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.player.GetComponent<PlayerController>().currSpeed > 0)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
