using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAbility", menuName = "ScriptableObjects/BTSubtrees/Default/Ability")]
public class DefaultAbility : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        #region CREATURE ABILITIES
            #region player triggered ability sequence        
                List<BTNode> AbilityTriggeredSequenceList = new List<BTNode>();

                #region Ability Selector
                    List<BTNode> AbilitySelectorList = new List<BTNode>();
                    CActionAbilityFail AbilityFailsafe = new CActionAbilityFail("Ability Failsafe", context);
                    #region MeleeAbilitySequence
                        List<BTNode> MeleeAbilitySequenceList = new List<BTNode>();
                        #region if target already exists selector
                            List<BTNode> TargetExistsSelectorList = new List<BTNode>();
                            CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
                            CActionFindTargetEnemy findTargetEnemy = new CActionFindTargetEnemy("Find Closest Enemy in Range", context);
                            TargetExistsSelectorList.Add(checkIfTargetExists);
                            TargetExistsSelectorList.Add(findTargetEnemy);

                            BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
                        #endregion 

                        #region Approach sequence
                            List<BTNode> MeleeApproachSelectorList = new List<BTNode>();
                            //BTCheckDistanceToTarget checkIfDistanceToTarget = new BTCheckDistanceToTarget("Check if in range for attack", context);
                            CActionApproachForAttack approachForAttack = new CActionApproachForAttack("Approach for attack", context);
                            BTInverter invertApproachForAttack = new BTInverter("Invert Approach for Attack", approachForAttack);
                            CActionAttackMelee attackMelee = new CActionAttackMelee("Melee Attack", context);
                            //MeleeApproachSequenceList.Add(checkIfDistanceToTarget);
                            MeleeApproachSelectorList.Add(invertApproachForAttack);
                            MeleeApproachSelectorList.Add(attackMelee);
                            
                            BTSelector MeleeApproachAttackSelector = new BTSelector("Melee Approach / Attack Sequence", MeleeApproachSelectorList);
                        #endregion


                        CCheckIfAbilityIsMelee ifAbilityIsMelee = new CCheckIfAbilityIsMelee("check if ability is melee", context);
                        
                        MeleeAbilitySequenceList.Add(ifAbilityIsMelee);
                        MeleeAbilitySequenceList.Add(TargetExistsSelector);
                        MeleeAbilitySequenceList.Add(MeleeApproachAttackSelector);
                        //MeleeAbilitySequenceList.Add(MeleeAttackSequence);
                        //MeleeAbilitySequenceList.Add(AbilityFailsafe);
                        BTSequence MeleeAbilitySequence = new BTSequence("Melee Ability Sequence", MeleeAbilitySequenceList);

                    #endregion

                    #region RangedAbilitySequence
                        List<BTNode> RangedAbilitySequenceList = new List<BTNode>();
                        #region if target already exists selector
                            //using same targetExistsSelector from melee
                        #endregion 

                        #region Approach/Attack sequence
                            List<BTNode> RangedApproachSelectorList = new List<BTNode>();
                            //using same invertApproachForAttack node
                            CActionAttackRanged attackRanged = new CActionAttackRanged("Ranged Attack", context);
                            RangedApproachSelectorList.Add(invertApproachForAttack);
                            RangedApproachSelectorList.Add(attackRanged);

                            BTSelector RangedApproachAttackSelector = new BTSelector("Ranged Approach / Attack Seelector", RangedApproachSelectorList);
                        #endregion
                        CCheckIfAbilityIsRanged ifAbilityIsRanged = new CCheckIfAbilityIsRanged("Check if ability is ranged", context);

                        RangedAbilitySequenceList.Add(ifAbilityIsRanged);
                        RangedAbilitySequenceList.Add(TargetExistsSelector);
                        RangedAbilitySequenceList.Add(RangedApproachAttackSelector);

                        BTSequence RangedAbilitySequence = new BTSequence("Ranged Ability Sequence", RangedAbilitySequenceList);
                    #endregion

                    #region UtilityAbilitySequence
                        List<BTNode> UtilityAblitySequenceList = new List<BTNode>();

                        CCheckIfAbilityIsUtility ifAbilityIsUtility = new CCheckIfAbilityIsUtility("Check if ability is utility", context);
                        //MAKE UTILITY ABLITY CAST

                        UtilityAblitySequenceList.Add(ifAbilityIsUtility);
                        //add action

                        BTSequence UtilityAbilitySequence = new BTSequence("Utility Ability Sequence", UtilityAblitySequenceList);

                        
                    #endregion
                    
                    AbilitySelectorList.Add(MeleeAbilitySequence);
                    AbilitySelectorList.Add(RangedAbilitySequence);
                    //AbilitySelectorList.Add(UtilityAbilitySequence);
                    AbilitySelectorList.Add(AbilityFailsafe);

                    BTSelector AbilitySelector = new BTSelector("Ability Selector", AbilitySelectorList);
                #endregion
                CCheckPlayerTriggeredAbility ifPlayerTriggeredAbility = new CCheckPlayerTriggeredAbility("if player triggered ability", context);
                CCheckIfAbilityOnCd ifAbilityOnCd = new CCheckIfAbilityOnCd("Check if Ability is on cooldown", context);
                AbilityTriggeredSequenceList.Add(ifPlayerTriggeredAbility);
                AbilityTriggeredSequenceList.Add(ifAbilityOnCd);
                AbilityTriggeredSequenceList.Add(AbilitySelector);

                BTSequence AbilityTriggeredSequence = new BTSequence("Ability Triggered Sequence", AbilityTriggeredSequenceList);
            #endregion
        #endregion
        return AbilityTriggeredSequence;
    }
}
