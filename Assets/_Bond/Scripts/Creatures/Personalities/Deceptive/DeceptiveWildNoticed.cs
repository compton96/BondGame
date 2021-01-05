// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeceptiveWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Deceptive/WildNoticed")]
public class DeceptiveWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        #region WILD PLAYER
            #region wild approach player
                List<BTNode> WildApproachPlayerList = new List<BTNode>();
                CActionWildApproachPlayer approachPlayer = new CActionWildApproachPlayer("Approach Player", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
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
