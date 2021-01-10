//Eugene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergeticWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Energetic/WildNoticed")]
public class EnergeticWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        //DEFAULT BT SUBTREE
        #region WILD PLAYER
            #region wild player scary
                List<BTNode> WildPlayerScaryList = new List<BTNode>();
                CCheckEnergeticWildPlayerScary playerScary = new CCheckEnergeticWildPlayerScary("ENERGETIC Player Scary", context);
                CActionEnergeticWildRunFromPlayer runFromPlayer = new CActionEnergeticWildRunFromPlayer("ENERGETIC Run From Player", context);
                WildPlayerScaryList.Add(playerScary);
                WildPlayerScaryList.Add(runFromPlayer);
                BTSequence playerScarySequence = new BTSequence("ENERGETIC Is Player Scary", WildPlayerScaryList);
            #endregion

            #region wild approach player
                List<BTNode> WildApproachPlayerList = new List<BTNode>();
                CActionEnergeticWildWanderInLocation wildWander = new CActionEnergeticWildWanderInLocation("ENERGETIC Wander", context);
                CActionEnergeticWildWanderIdle wildWanderIdle = new CActionEnergeticWildWanderIdle("ENERGETIC Wander Idle", context);
                WildApproachPlayerList.Add(playerScarySequence);
                WildApproachPlayerList.Add(wildWander);
                WildApproachPlayerList.Add(wildWanderIdle);
                BTSelector approachPlayerSelector = new BTSelector("ENERGETIC Run/Approach Player", WildApproachPlayerList);
            #endregion

            #region wild notice player
                List<BTNode> WildNoticePlayerList = new List<BTNode>();
                CCheckWildPlayerInRadius playerNoticed = new CCheckWildPlayerInRadius("ENERGETIC Is Player Noticed", context);
                WildNoticePlayerList.Add(playerNoticed);
                WildNoticePlayerList.Add(approachPlayerSelector);
                BTSequence noticedSequence = new BTSequence("ENERGETIC Player Is Noticed", WildNoticePlayerList);
            #endregion
        #endregion
        return noticedSequence;
    }
}
