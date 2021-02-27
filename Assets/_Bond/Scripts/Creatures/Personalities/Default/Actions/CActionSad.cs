using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionSad : BTLeaf
{
    public CActionSad(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.enthusiasmInteracted = false;
        context.interactRadius.SetActive(true);
        context.animator.Sad();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate()
    {
        OnParentExit();
        return NodeState.SUCCESS;
    }

}
