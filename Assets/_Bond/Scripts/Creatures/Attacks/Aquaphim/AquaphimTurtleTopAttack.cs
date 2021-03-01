using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AquaphimTurtleTop", menuName = "ScriptableObjects/BTSubtrees/Attacks/Aquaphim/Turtle Top")]
public class AquaphimTurtleTopAttack : BTSubtree
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
            List<BTNode> meleeApproachSelectorList = new List<BTNode>();
            CActionApproachForAttack approachForAttack = new CActionApproachForAttack("Approach for attack", context);
            BTInverter invertApproachForAttack = new BTInverter("Invert Approach for Attack", approachForAttack);
            CActionAttackTurtleTop attackTurtleTop = new CActionAttackTurtleTop("Turtle Top Attack", context);
            meleeApproachSelectorList.Add(invertApproachForAttack);
            meleeApproachSelectorList.Add(attackTurtleTop);
            
            BTSelector MeleeApproachAttackSelector = new BTSelector("Melee Approach / Attack Sequence", meleeApproachSelectorList);
        #endregion

        AbilitySequenceList.Add(TargetExistsSelector);
        AbilitySequenceList.Add(MeleeApproachAttackSelector);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
