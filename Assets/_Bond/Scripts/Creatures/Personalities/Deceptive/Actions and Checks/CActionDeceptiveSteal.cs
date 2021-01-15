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
        //Play anim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.resetStealTimer();

        return NodeState.FAILURE;
    }
}
