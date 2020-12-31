//Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckAdventurousWildPlayerScary : BTChecker
{

    public CCheckAdventurousWildPlayerScary(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        if (context.player.GetComponent<PlayerController>().currSpeed > context.playerSpeedToScare * 1.5)
        {
            //we could expand on this check; rn it just checks if player is moving too fast for the creature
            //maybe make it dependent on creature personality?
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
