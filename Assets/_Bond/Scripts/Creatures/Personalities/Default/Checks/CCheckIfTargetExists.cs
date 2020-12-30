using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCheckIfTargetExists : BTChecker
{
    public CCheckIfTargetExists(string _name, CreatureAIContext _context) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("CHECKING IF TARGET EXISTS : " + context.targetEnemy);
        //return NodeState.FAILURE;
        
        if(context.targetEnemy == null)
        {
            return NodeState.FAILURE;
        }
        return NodeState.SUCCESS;
    }
}
