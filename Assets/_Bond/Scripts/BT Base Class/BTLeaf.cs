using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTLeaf : BTNode
{
    protected CreatureAIContext context;
    protected bool ranOnEnter = false;

    protected BTLeaf(string _name, CreatureAIContext _context) : base(_name)
    {
        name = _name;
        context = _context;
    }

    public abstract override NodeState Evaluate();

    protected abstract void OnEnter();

    protected abstract void OnExit();
}
