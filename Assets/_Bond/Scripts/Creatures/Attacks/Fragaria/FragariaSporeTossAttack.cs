using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FragariaSporeTossAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Fragaria/Spore Toss")]
public class FragariaSporeTossAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        CActionAttackSporeToss attackSporeToss = new CActionAttackSporeToss("Spore Toss Attack", context);

        AbilitySequenceList.Add(attackSporeToss);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
