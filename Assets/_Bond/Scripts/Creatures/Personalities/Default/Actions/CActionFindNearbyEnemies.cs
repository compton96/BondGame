using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionFindNearbyEnemies : BTLeaf
{
    
    public CActionFindNearbyEnemies(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() {
        // Debug.Log("FINDING TARGET ENEMIES");
        int layermask = 1 << 8; //only layer 8 will be targeted
        Collider[] hitColliders = Physics.OverlapSphere(context.creatureTransform.position, context.enemyDetectRange, layermask);
        List<GameObject> nearbyEnemyList = new List<GameObject>();
        GameObject closestEnemy = null;
        float closestDistance = 100;

        foreach (var hitCollider in hitColliders)
        { 
            var distance = Vector3.Distance(hitCollider.gameObject.transform.position, context.creatureTransform.position);
            nearbyEnemyList.Add(hitCollider.gameObject);
            if(distance < closestDistance) 
            {
                closestDistance = distance;
                closestEnemy = hitCollider.gameObject;
            }
        }
        
        if(closestEnemy != null) 
        {
            context.targetEnemy = closestEnemy;
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
        OnParentExit();
        return NodeState.FAILURE;

    }
}
