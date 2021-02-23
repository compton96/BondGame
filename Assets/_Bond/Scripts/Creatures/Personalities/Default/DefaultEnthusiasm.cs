using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnthusiasm : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region Enthusiasm Management
            List<BTNode> enthusiasmSelectorList = new List<BTNode>();

            #region Enthusiasm too low
                List<BTNode> lowEnthusiasmSequenceList = new List<BTNode>();

                CCheckEnthusiasmLow checkEnthusiasmLow = new CCheckEnthusiasmLow("Check if enthusiasm is low", context);

                #region Sad -> interacted -> Boost Enthusiasm
                    List<BTNode> isSadSelectorList = new List<BTNode>();

                    #region Combat Sad
                        List<BTNode> combatList = new List<BTNode>();
                        CCheckInCombat checkInCombat = new CCheckInCombat("Check in combat", context);
                        CActionOutOfEnthusiasm outOfEnthusiasm = new CActionOutOfEnthusiasm("out of enthusiasm animation", context);
                        combatList.Add(checkInCombat);
                        combatList.Add(outOfEnthusiasm);
                        BTSequence combatSequence = new BTSequence("Combat", combatList);
                    #endregion


                    #region Player interacted
                        List<BTNode> interactList = new List<BTNode>();
                        //Check if player HASNT interacted
                        //do sad animation - and enable interaction colliders or something
                        //InteractList.Add(CCheckInteracted);
                        //InteractList.Add(CActionSad);
                        BTSequence interactSequence = new BTSequence("Not Interacted with", interactList);
                    #endregion

                    CActionEnthusiasmUp enthusiasmUp = new CActionEnthusiasmUp("Boost Enthusiasm", context);

                    isSadSelectorList.Add(combatSequence);
                    isSadSelectorList.Add(interactSequence);
                    isSadSelectorList.Add(enthusiasmUp);


                    BTSelector isSadSelector = new BTSelector("Enthusiasm ", isSadSelectorList);
                #endregion
                
                lowEnthusiasmSequenceList.Add(checkEnthusiasmLow);
                lowEnthusiasmSequenceList.Add(isSadSelector);
                BTSequence enthusiasmSequence = new BTSequence("Not Interacted with", lowEnthusiasmSequenceList);
            #endregion
            
            #region Enthusiasm not too low combat check
                List<BTNode> combatEnthusiasmIncreaseList = new List<BTNode>();
     
                CActionPassiveEnthusiasmGain passiveEnthusiasmGain = new CActionPassiveEnthusiasmGain("Passive enthusiasm gain", context);
                BTInverter increaseEnthusiasmInverter = new BTInverter("Not in Combat", passiveEnthusiasmGain);
                BTInverter inCombatInverter = new BTInverter("Not in Combat",  checkInCombat);
                combatEnthusiasmIncreaseList.Add(inCombatInverter);
                combatEnthusiasmIncreaseList.Add(increaseEnthusiasmInverter);

                BTSequence combatEnthusiasmIncrease = new BTSequence("Enthusiasm increase over time", combatEnthusiasmIncreaseList);

            #endregion
            enthusiasmSelectorList.Add(enthusiasmSequence);
            enthusiasmSelectorList.Add(combatEnthusiasmIncrease);
        #endregion

        BTSelector Enthusiasm = new BTSelector("Enthusiasm selector",enthusiasmSelectorList);
        return Enthusiasm;
    }
}
