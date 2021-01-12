using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTLeaf : BTNode
{
    protected CreatureAIContext context;
    protected EnemyAIContext enemyContext;
    // protected bool ranOnEnter = false; // DELETE LATER
    protected bool currentlyRunning = false;

    protected BTLeaf(string _name, CreatureAIContext _context) : base(_name)
    {
        name = _name;
        context = _context;
    }

    public BTLeaf(string _name, EnemyAIContext _context) : base(_name) {
        name = _name;
        enemyContext = _context;
    }

    public abstract override NodeState Evaluate();

    protected abstract void OnEnter();

    protected abstract void OnExit();

    public void OnParentEnter()
    {
        currentlyRunning = true;
        OnEnter();
    }

    public void OnParentExit()
    {
        currentlyRunning = false;
        OnExit();
    }

    public override NodeState OnParentEvaluate()
    {
        if ( !currentlyRunning )
        {
            OnParentEnter();
        }
        return Evaluate();
    }
}
