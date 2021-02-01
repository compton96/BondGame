// Enrico
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionWildWanderIdle : BTLeaf
{
    public CActionWildWanderIdle(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.wanderIdleTimer = 0;
        context.wanderIdling = true;
        context.wanderIdleDuration = Random.Range(2f, 3f);
    }

    protected override void OnExit()
    {
        context.wanderIdleTimer = 0;
        context.wanderIdling = false;
    }

    public override NodeState Evaluate()
    {
        context.doMovement(0);
        context.wanderIdleTimer += Time.deltaTime;
        if (context.wanderIdleTimer >= context.wanderIdleDuration) 
        {
            OnParentExit();
            return NodeState.SUCCESS;
        }
        context.updateDebugText(name);
        return NodeState.RUNNING;
    }
}