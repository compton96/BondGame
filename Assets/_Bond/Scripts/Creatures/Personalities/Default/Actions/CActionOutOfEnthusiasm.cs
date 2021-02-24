using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionOutOfEnthusiasm : BTLeaf
{
    public CActionOutOfEnthusiasm(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Play anim
        context.animator.Wave();
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        
        if(true) 
        { //if animation done, have to add that 
            
            context.doMovement(0);
            OnParentExit();
            return NodeState.SUCCESS;
        }
        

    }
}
