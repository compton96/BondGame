using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FragariaPetalThrowAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Fragaria/Petal Throw")]
public class FragariaPetalThrowAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        #region if target already exists selector
            List<BTNode> TargetExistsSelectorList = new List<BTNode>();
            CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
            CActionFindTargetEnemy findTargetEnemy = new CActionFindTargetEnemy("Find Closest Enemies in Range", context);
            TargetExistsSelectorList.Add(checkIfTargetExists);
            TargetExistsSelectorList.Add(findTargetEnemy);

            BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
        #endregion 


        #region Approach and attack selector
            List<BTNode> RangedApproachSelectorList = new List<BTNode>();
            CActionApproachForAttack approachForAttack = new CActionApproachForAttack("Approach for attack", context);
            BTInverter invertApproachForAttack = new BTInverter("Invert Approach for Attack", approachForAttack);
            CActionAttackPetalThrow attackPetalThrow = new CActionAttackPetalThrow("Petal Throw Attack", context);
            RangedApproachSelectorList.Add(invertApproachForAttack);
            RangedApproachSelectorList.Add(attackPetalThrow);
            
            BTSelector RangedApproachAttackSelector = new BTSelector("Melee Approach / Attack Sequence", RangedApproachSelectorList);
        #endregion

        AbilitySequenceList.Add(TargetExistsSelector);
        AbilitySequenceList.Add(RangedApproachAttackSelector);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
