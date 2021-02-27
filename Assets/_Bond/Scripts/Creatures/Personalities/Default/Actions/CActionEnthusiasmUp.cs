using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionEnthusiasmUp : BTLeaf
{
    public CActionEnthusiasmUp(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Play anim
        context.animator.Jump();
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        
        if(true) 
        { //if animation done, have to add that 
            context.enthusiasmInteracted = false;
            context.creatureStats.statManager.setStat(ModiferType.CURR_ENTHUSIASM, context.creatureStats.statManager.getStat(ModiferType.MAX_ENTHUSIASM) * 0.1f);
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
    }
}
