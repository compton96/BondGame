// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionDeceptiveSteal : BTLeaf
{
    public CActionDeceptiveSteal(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        ranOnEnter = true;
        //Play amim
    }

    protected override void OnExit()
    {
        ranOnEnter = false;
    }

    public override NodeState Evaluate() 
    {
        if(!ranOnEnter)
        {
            OnEnter();
        }

        context.resetStealTimer();

        return NodeState.FAILURE;
    }
}
