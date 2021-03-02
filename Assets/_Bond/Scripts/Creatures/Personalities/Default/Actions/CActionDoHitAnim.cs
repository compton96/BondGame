using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionDoHitAnim : BTLeaf
{
    public CActionDoHitAnim(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Play anim
        context.animator.Attack1();
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        
        if(true) 
        { //if animation done, have to add that 
            
            
            OnParentExit();
            return NodeState.SUCCESS;
        }
        

    }
}
