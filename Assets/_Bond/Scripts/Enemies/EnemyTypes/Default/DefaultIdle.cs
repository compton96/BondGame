// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultIdle", menuName = "ScriptableObjects/BTEnemySubtrees/Default/Idle")]
public class DefaultIdle : BTEnemySubtree
{
    public override BTSequence BuildSequenceSubtree(EnemyAIContext context) 
    {
        List<BTNode> idleList = new List<BTNode>();
        EActionReturnToSpawn returnToSpawn = new EActionReturnToSpawn("Return to Spawn", context);
        EActionIdle idle = new EActionIdle("Idle", context);
        idleList.Add(returnToSpawn);
        idleList.Add(idle);
        BTSequence idleSequence = new BTSequence("Idle Sequence", idleList);

        return idleSequence;
    }
}
