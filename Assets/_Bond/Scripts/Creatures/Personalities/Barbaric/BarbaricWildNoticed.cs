using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BarbaricWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Barbaric/WildNoticed")]
public class BarbaricWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context)
    {
        //DEFAULT BT SUBTREE
        #region WILD PLAYER

            #region wild attack enemy
                List<BTNode> AttackEnemySequenceList = new List<BTNode>();

                #region if target already exists selector
                    List<BTNode> TargetExistsSelectorList = new List<BTNode>();
                    CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
                    CActionFindTargetEnemy findTargetEnemy = new CActionFindTargetEnemy("Find Closest Enemy in Range", context);
                    TargetExistsSelectorList.Add(checkIfTargetExists);
                    TargetExistsSelectorList.Add(findTargetEnemy);

                    BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
                #endregion 

                #region attack selector
                    List<BTNode> AttackSelectorList = new List<BTNode>();

                    #region melee Sequence
                        List<BTNode> MeleeAbilitySequenceList = new List<BTNode>();
                        #region Melee Approach selector
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
                        MeleeAbilitySequenceList.Add(MeleeApproachAttackSelector);

                        BTSequence MeleeAbilitySequence = new BTSequence("Melee Ability Sequence", MeleeAbilitySequenceList);
                    #endregion

                    #region ranged sequence
                        List<BTNode> RangedAbilitySequenceList = new List<BTNode>();
                        #region Ranged Approach selector
                            List<BTNode> RangedApproachSelectorList = new List<BTNode>();
                            //using same invertApproachForAttack node
                            CActionAttackRanged attackRanged = new CActionAttackRanged("Ranged Attack", context);
                            RangedApproachSelectorList.Add(invertApproachForAttack);
                            RangedApproachSelectorList.Add(attackRanged);

                            BTSelector RangedApproachAttackSelector = new BTSelector("Ranged Approach / Attack Seelector", RangedApproachSelectorList);
                        #endregion
                        CCheckIfAbilityIsRanged ifAbilityIsRanged = new CCheckIfAbilityIsRanged("Check if ability is ranged", context);

                        RangedAbilitySequenceList.Add(ifAbilityIsRanged);
                        RangedAbilitySequenceList.Add(RangedApproachAttackSelector);

                        BTSequence RangedAbilitySequence = new BTSequence("Ranged Ability Sequence", RangedAbilitySequenceList);
                    #endregion

                    AttackSelectorList.Add(MeleeAbilitySequence);
                    AttackSelectorList.Add(RangedAbilitySequence);

                    BTSelector AttackSelector = new BTSelector("Attack Selector", AttackSelectorList);
                #endregion

                AttackEnemySequenceList.Add(TargetExistsSelector);
                AttackEnemySequenceList.Add(AttackSelector);

                BTSequence AttackEnemySequence = new BTSequence("Attack Enemy Sequence", AttackEnemySequenceList);


            #endregion

            #region wild approach player
                List<BTNode> WildApproachPlayerList = new List<BTNode>();
                CActionWildApproachPlayer approachPlayer = new CActionWildApproachPlayer("Approach Player", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
                WildApproachPlayerList.Add(AttackEnemySequence);
                WildApproachPlayerList.Add(approachPlayer);
                WildApproachPlayerList.Add(followIdle);
                BTSelector approachPlayerSelector = new BTSelector("Run/Approach Player", WildApproachPlayerList);
            #endregion

            #region wild notice player
                List<BTNode> WildNoticePlayerList = new List<BTNode>();
                CCheckWildPlayerInRadius playerNoticed = new CCheckWildPlayerInRadius("Is Player Noticed", context);
                
                WildNoticePlayerList.Add(playerNoticed);
                WildNoticePlayerList.Add(approachPlayerSelector);
                BTSequence noticedSequence = new BTSequence("Player Is Noticed", WildNoticePlayerList);
            #endregion
        #endregion
        return noticedSequence;
    }
}
