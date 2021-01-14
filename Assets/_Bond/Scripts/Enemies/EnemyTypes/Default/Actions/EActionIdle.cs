using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionIdle : BTLeaf
{
    public EActionIdle(string _name, EnemyAIContext _context ) : base(_name, _context)
    {

    }

    protected override void OnEnter()
    {
        //Play awake anim
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        return NodeState.RUNNING;
    }
}
