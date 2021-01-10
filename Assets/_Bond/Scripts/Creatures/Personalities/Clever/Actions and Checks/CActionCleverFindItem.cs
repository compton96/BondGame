using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionCleverFindItem : BTLeaf
{
    
    public CActionCleverFindItem(string _name, CreatureAIContext _context ) : base(_name, _context)
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
        if (!ranOnEnter)
        {
            OnEnter();
        }

        if (context.cleverIgnoreItems)
        {
            OnExit();
            return NodeState.FAILURE;
        }

        int layermask = 1 << 10; //only layer 10 will be targeted
        Collider[] hitColliders = Physics.OverlapSphere(context.creatureTransform.position, context.itemDetectRange, layermask);
        GameObject closestItem = null;
        float closestDistance = 100;
        foreach (var hitCollider in hitColliders)
        { 
            var distance = Vector3.Distance(hitCollider.gameObject.transform.position, context.creatureTransform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = hitCollider.gameObject;
            }
        }
        if (closestItem != null)
        {
            context.cleverItem = closestItem;
            OnExit();
            return NodeState.SUCCESS;
        }
        
        OnExit();
        return NodeState.FAILURE;

    }
}
