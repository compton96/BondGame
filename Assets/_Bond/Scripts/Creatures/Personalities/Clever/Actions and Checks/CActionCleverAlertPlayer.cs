using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionCleverAlertPlayer : BTLeaf
{
    
    public CActionCleverAlertPlayer(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        context.animator.Wave();
    }

    protected override void OnExit()
    {
        context.cleverIgnoreItems = true;
        //find a better way to turn off cleverIgnoreItems
    }

    public override NodeState Evaluate()
    {
        Quaternion desiredLook = Quaternion.LookRotation(context.player.transform.position - context.creatureTransform.position, Vector3.up);
        desiredLook.x = 0;
        context.creatureTransform.rotation = desiredLook;

        if (context.cleverItem != null && Vector3.Distance(context.creatureTransform.position, context.player.transform.position) < 12)
        {
            return NodeState.RUNNING;
        }
        else
        {
            OnParentExit();
            return NodeState.SUCCESS;
        }
    }
}
