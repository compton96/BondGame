// Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeceptiveFollowPlayer", menuName = "ScriptableObjects/BTSubtrees/Deceptive/FollowPlayer")]
public class DeceptiveFollowPlayer : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region BONDED FOLLOW PLAYER
            List<BTNode> BondedFollowPlayer = new List<BTNode>();

            #region DONT STEAL PLAYER
                List<BTNode> DontStealPlayerList = new List<BTNode>();
        
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
                
                DontStealPlayerList.Add(followPlayerIdle);
                DontStealPlayerList.Add(followPlayerTrail);
                DontStealPlayerList.Add(followPlayerSelector);
            
                BTSelector DontStealPlayerSelector = new BTSelector("Dont Steal Player", DontStealPlayerList);

            #endregion

            #region STEAL FROM PLAYER
                List<BTNode> StealFromPlayerList = new List<BTNode>();

                CCheckDeceptiveShouldSteal shouldSteal = new CCheckDeceptiveShouldSteal("Should Steal", context);
                CActionDeceptiveSteal steal = new CActionDeceptiveSteal("Steal", context);

                StealFromPlayerList.Add(shouldSteal);
                StealFromPlayerList.Add(steal);

                BTSequence stealFromPlayerSequence = new BTSequence("Steal From Player", StealFromPlayerList);
            #endregion

            BondedFollowPlayer.Add(stealFromPlayerSequence);
            BondedFollowPlayer.Add(DontStealPlayerSelector);

            BTSelector bondedFollowPlayerSelector = new BTSelector("Bonded Follow Player", BondedFollowPlayer);
        #endregion
        return bondedFollowPlayerSelector;
    }
}
