// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdventurousWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Adventurous/WildNoticed")]
public class AdventurousWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        #region WILD PLAYER
            #region wild player scary
                List<BTNode> wildPlayerScaryList = new List<BTNode>();
                CCheckAdventurousWildPlayerScary playerScary = new CCheckAdventurousWildPlayerScary("Player Scary", context);
                CActionWildRunFromPlayer runFromPlayer = new CActionWildRunFromPlayer("Run From Player", context);
                wildPlayerScaryList.Add(playerScary);
                wildPlayerScaryList.Add(runFromPlayer);
                BTSequence playerScarySequence = new BTSequence("Is Player Scary", wildPlayerScaryList);
            #endregion

            #region wild approach player
                List<BTNode> wildApproachPlayerList = new List<BTNode>();
                CActionWildApproachPlayer approachPlayer = new CActionWildApproachPlayer("Approach Player", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
                wildApproachPlayerList.Add(playerScarySequence);
                wildApproachPlayerList.Add(approachPlayer);
                wildApproachPlayerList.Add(followIdle);
                BTSelector approachPlayerSelector = new BTSelector("Run/Approach Player", wildApproachPlayerList);
            #endregion

            #region wild notice player
                List<BTNode> wildNoticePlayerList = new List<BTNode>();
                CCheckWildPlayerInRadius playerNoticed = new CCheckWildPlayerInRadius("Is Player Noticed", context);
                wildNoticePlayerList.Add(playerNoticed);
                wildNoticePlayerList.Add(approachPlayerSelector);
                BTSequence noticedSequence = new BTSequence("Player Is Noticed", wildNoticePlayerList);
            #endregion
        #endregion
        return noticedSequence;
    }
}
