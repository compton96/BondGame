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

            #region wild run from player
                List<BTNode> WildApproachPlayerList = new List<BTNode>();
                CActionWildRunFromPlayer runFromPlayer = new CActionWildRunFromPlayer("Run From Player", context);
                CActionFollowIdle idle = new CActionFollowIdle("Follow Idle", context);
                WildApproachPlayerList.Add(runFromPlayer);
                WildApproachPlayerList.Add(idle);
                BTSelector runFromPlayerSelector = new BTSelector("Run from Player", WildApproachPlayerList);
            #endregion

            #region wild notice player
                List<BTNode> WildNoticePlayerList = new List<BTNode>();
                CCheckWildPlayerInRadius playerNoticed = new CCheckWildPlayerInRadius("Is Player Noticed", context);
                WildNoticePlayerList.Add(playerNoticed);
                WildNoticePlayerList.Add(runFromPlayerSelector);
                BTSequence noticedSequence = new BTSequence("Player Is Noticed", WildNoticePlayerList);
            #endregion
        #endregion
        return noticedSequence;
    }
}
