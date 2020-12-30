using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionFollowTP : BTLeaf
{
    public CActionFollowTP(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        ranOnEnter = true;
    }

    protected override void OnExit()
    {
        ranOnEnter = false;
    }

    public override NodeState Evaluate()
    {
        context.creatureTransform.position = context.backFollowPoint.transform.position;
        return NodeState.SUCCESS;
    }


}