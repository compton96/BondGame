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

                    #region ability 1
                        List<BTNode> AbilityOneSequenceList = new List<BTNode>();
                        CCheckAbilityOne checkAbilityOne = new CCheckAbilityOne("Check ability 1", context);
                        BTSequence AbilityOneSubtree = context.creatureStats.abilities[0].abilityBehavior.BuildSequenceSubtree(context);
                        AbilityOneSequenceList.Add(checkAbilityOne);
                        AbilityOneSequenceList.Add(AbilityOneSubtree);
                        BTSequence AbilityOneSequence = new BTSequence("Ability one", AbilityOneSequenceList);
                    #endregion

                    #region ability 2
                        List<BTNode> AbilityTwoSequenceList = new List<BTNode>();
                        CCheckAbilityTwo checkAbilityTwo = new CCheckAbilityTwo("Check ability 2", context);
                        BTSequence AbilityTwoSubtree = context.creatureStats.abilities[1].abilityBehavior.BuildSequenceSubtree(context);
                        AbilityTwoSequenceList.Add(checkAbilityTwo);
                        AbilityTwoSequenceList.Add(AbilityTwoSubtree);
                        BTSequence AbilityTwoSequence = new BTSequence("Ability one", AbilityTwoSequenceList);
                    #endregion

                    AbilitySelectorList.Add(AbilityOneSequence);
                    AbilitySelectorList.Add(AbilityTwoSequence);

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
