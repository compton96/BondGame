using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionFindTargetEnemy : BTLeaf
{
    
    public CActionFindTargetEnemy(string _name, CreatureAIContext _context ) : base(_name, _context)
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

    public override NodeState Evaluate() {
        if(!ranOnEnter)
        {
            OnEnter();
        }

        Debug.Log("FINDING TARGET ENEMIES");
        int layermask = 1 << 8; //only layer 8 will be targeted
        Collider[] hitColliders = Physics.OverlapSphere(context.creatureTransform.position, context.enemyDetectRange, layermask);
        GameObject closestEnemy = null;
        float closestDistance = 100;

        foreach (var hitCollider in hitColliders)
        { 
            var distance = Vector3.Distance(hitCollider.gameObject.transform.position, context.creatureTransform.position);
            if(distance < closestDistance) 
            {
                closestDistance = distance;
                closestEnemy = hitCollider.gameObject;
            }
            
           
        }
        
        if(closestEnemy != null) 
        {
            context.targetEnemy = closestEnemy;
            OnExit();
            return NodeState.SUCCESS;

        }
        
        OnExit();
        return NodeState.FAILURE;

    }
}
