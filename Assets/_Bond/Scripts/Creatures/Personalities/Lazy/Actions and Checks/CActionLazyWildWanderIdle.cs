// Eugene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionLazyWildWanderIdle : BTLeaf
{
    public CActionLazyWildWanderIdle(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.wanderIdleTimer = 0;
        context.wanderIdling = true;
        context.wanderIdleDuration = Random.Range(4f, 6f);
        context.animator.LayDown();
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
        return NodeState.RUNNING;
    }
}