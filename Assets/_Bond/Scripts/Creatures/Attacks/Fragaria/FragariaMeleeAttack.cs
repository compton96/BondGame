using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FragariaMeleeAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Fragaria/Melee")]
public class FragariaMeleeAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        #region if target already exists selector
            List<BTNode> TargetExistsSelectorList = new List<BTNode>();
            CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
            CActionFindTargetEnemy findTargetEnemy = new CActionFindTargetEnemy("Find Closest Enemy in Range", context);
            TargetExistsSelectorList.Add(checkIfTargetExists);
            TargetExistsSelectorList.Add(findTargetEnemy);

            BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
        #endregion 


         #region Approach and attack sequence
            List<BTNode> MeleeApproachSelectorList = new List<BTNode>();
            //BTCheckDistanceToTarget checkIfDistanceToTarget = new BTCheckDistanceToTarget("Check if in range for attack", context);
            CActionApproachForBasicMelee approachForAttack = new CActionApproachForBasicMelee("Approach for attack", context);
            BTInverter invertApproachForAttack = new BTInverter("Invert Approach for Attack", approachForAttack);
            CActionAttackMelee attackMelee = new CActionAttackMelee("Melee Attack", context);
            //MeleeApproachSequenceList.Add(checkIfDistanceToTarget);
            MeleeApproachSelectorList.Add(invertApproachForAttack);
            MeleeApproachSelectorList.Add(attackMelee);
            
            BTSelector MeleeApproachAttackSelector = new BTSelector("Melee Approach / Attack Sequence", MeleeApproachSelectorList);
        #endregion

        AbilitySequenceList.Add(TargetExistsSelector);
        AbilitySequenceList.Add(MeleeApproachAttackSelector);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
