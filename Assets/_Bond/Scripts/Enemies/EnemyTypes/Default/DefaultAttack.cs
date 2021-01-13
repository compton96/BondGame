using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttack", menuName = "ScriptableObjects/BTEnemySubtrees/Default/Attack")]
public class DefaultAttack : BTEnemySubtree
{
    public override BTSequence BuildSequenceSubtree(EnemyAIContext context) 
    {
        List<BTNode> attackList = new List<BTNode>();
        EActionApproachPlayer approachPlayer = new EActionApproachPlayer("Approach Player", context);
        EActionAttackPlayer attackPlayer = new EActionAttackPlayer("Attack player", context);
        attackList.Add(approachPlayer);
        attackList.Add(attackPlayer);
        BTSequence attackSequence = new BTSequence("Attack Sequence", attackList);

        return attackSequence;
    }
}
