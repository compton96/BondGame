using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultFollowPlayer", menuName = "ScriptableObjects/BTSubtrees/Default/FollowPlayer")]
public class DefaultFollowPlayer : BTSubtree
{

    public override BTSelector BuildSelectorSubtree(CreatureAIContext context)
    {
        #region BONDED FOLLOW PLAYER
            List<BTNode> BondedFollowPlayer = new List<BTNode>();
        
            #region bonded follow idle sequence
                List<BTNode> BondedIdleFollowList = new List<BTNode>();
                CCheckInPlayerRadius inRadius = new CCheckInPlayerRadius("In Player Radius", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
                BondedIdleFollowList.Add(inRadius);
                BondedIdleFollowList.Add(followIdle);
                BTSequence followPlayerIdle = new BTSequence("Follow Player Idle", BondedIdleFollowList);
            #endregion

            #region bonded trail player sequence
                List<BTNode> BondedTrailPlayerList = new List<BTNode>();
                CCheckInPlayerTrail inTrail = new CCheckInPlayerTrail("In Player Trail", context);
                CActionTrailPlayer trailPlayer = new CActionTrailPlayer("Trail Player", context);
                BondedTrailPlayerList.Add(inTrail);
                BondedTrailPlayerList.Add(trailPlayer);
                BTSequence followPlayerTrail = new BTSequence("Follow Player Trail", BondedTrailPlayerList);
            #endregion
            
            #region bonded get closer to player selector
                List<BTNode> BondedGetCloserToPlayerList = new List<BTNode>();
                CActionFollowPlayer followPlayerAction = new CActionFollowPlayer("Follow Player", context);
                CActionFollowTP followPlayerTP = new CActionFollowTP("Follow Player TP", context);
                BondedGetCloserToPlayerList.Add(followPlayerAction);
                BondedGetCloserToPlayerList.Add(followPlayerTP);
                BTSelector followPlayerSelector = new BTSelector("Get Closer To Player", BondedGetCloserToPlayerList);
            #endregion
            
            BondedFollowPlayer.Add(followPlayerIdle);
            BondedFollowPlayer.Add(followPlayerTrail);
            BondedFollowPlayer.Add(followPlayerSelector);

            BTSelector bondedFollowPlayerSelector = new BTSelector("Bonded Follow Player", BondedFollowPlayer);
        #endregion
        return bondedFollowPlayerSelector;
    }
}
