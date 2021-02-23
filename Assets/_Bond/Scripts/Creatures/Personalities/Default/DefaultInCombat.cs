using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultInCombat : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context)
    {
        #region In Combat - sequence
            List<BTNode> inCombatSequenceList = new List<BTNode>();
            
            CCheckInCombat checkInCombat = new CCheckInCombat("check in combat", context);
            CCheckAutoAttack checkAutoAttack = new CCheckAutoAttack("check if autoattack", context);


            #region wait or attack selector
                List<BTNode> waitOrAttackSelectorList = new List<BTNode>();

                #region ability is on cd sequence
                    List<BTNode> abilityOnCDSequenceList = new List<BTNode>();
                    
                    CCheckDefaultAttackOnCD checkDefaultAttackOnCD = new CCheckDefaultAttackOnCD("Check if default on CD", context);
                    CActionFollowIdle followIdle = new CActionFollowIdle("idle", context);
                    
                    abilityOnCDSequenceList.Add(checkDefaultAttackOnCD);
                    abilityOnCDSequenceList.Add(followIdle);
                    BTSequence abilityOnCDSequence = new BTSequence("In combat Sequence", abilityOnCDSequenceList);
                #endregion

                #region Approach and attack sequence
                    BTSequence abilitySequence = context.basicCreatureAttack.abilityBehavior.BuildSequenceSubtree(context);
                #endregion

                waitOrAttackSelectorList.Add(abilityOnCDSequence);
                waitOrAttackSelectorList.Add(abilitySequence);

                BTSelector waitOrAttackSelector = new BTSelector("wait or attack selector", waitOrAttackSelectorList);
            #endregion
  
            inCombatSequenceList.Add(checkInCombat);
            inCombatSequenceList.Add(checkAutoAttack);
            inCombatSequenceList.Add(waitOrAttackSelector);
       
            BTSequence InCombatSequence = new BTSequence("In combat Sequence", inCombatSequenceList);
        #endregion

        return InCombatSequence;
    }
}
