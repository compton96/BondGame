// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrawnyWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Scrawny/WildNoticed")]
public class ScrawnyWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        #region WILD PLAYER
            #region wild player scary
                List<BTNode> WildPlayerScaryList = new List<BTNode>();
                CCheckScrawnyWildPlayerMoving playerMoving = new CCheckScrawnyWildPlayerMoving("Player Moving", context);
                CActionWildRunFromPlayer runFromPlayer = new CActionWildRunFromPlayer("Run From Player", context);
                WildPlayerScaryList.Add(playerMoving);
                WildPlayerScaryList.Add(runFromPlayer);
                BTSequence playerScarySequence = new BTSequence("Is Player Scary", WildPlayerScaryList);
            #endregion

            #region wild approach player
                List<BTNode> WildApproachPlayerList = new List<BTNode>();
                CActionWildApproachPlayer approachPlayer = new CActionWildApproachPlayer("Approach Player", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
                WildApproachPlayerList.Add(playerScarySequence);
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
