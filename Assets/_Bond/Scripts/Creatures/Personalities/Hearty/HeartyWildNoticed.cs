// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeartyWildNoticed", menuName = "ScriptableObjects/BTSubtrees/Hearty/WildNoticed")]
public class HeartyWildNoticed : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        #region WILD PLAYER

            #region wild player dropped food
                List<BTNode> WildDroppedFoodList = new List<BTNode>();
                CCheckHeartyWildDroppedFood droppedFood = new CCheckHeartyWildDroppedFood("Player Dropped Food", context);
                CActionHeartyWildApproachDroppedFood approachDroppedFood = new CActionHeartyWildApproachDroppedFood("Approach Dropped Food", context);
                WildDroppedFoodList.Add(droppedFood);
                WildDroppedFoodList.Add(approachDroppedFood);
                BTSequence droppedFoodSequence = new BTSequence("Player Dropped Food", WildDroppedFoodList);
            #endregion

            #region wild player scary
                List<BTNode> WildPlayerScaryList = new List<BTNode>();
                CActionWildRunFromPlayer runFromPlayer = new CActionWildRunFromPlayer("Run From Player", context);
                CActionFollowIdle followIdle = new CActionFollowIdle("Follow Idle", context);
                WildPlayerScaryList.Add(droppedFoodSequence);
                WildPlayerScaryList.Add(runFromPlayer);
                WildPlayerScaryList.Add(followIdle);
                BTSelector approachFoodSelector = new BTSelector("Run/Approach Food", WildPlayerScaryList);
            #endregion

            #region wild notice player
                List<BTNode> WildNoticePlayerList = new List<BTNode>();
                CCheckWildPlayerInRadius playerNoticed = new CCheckWildPlayerInRadius("Is Player Noticed", context);
                WildNoticePlayerList.Add(playerNoticed);
                WildNoticePlayerList.Add(approachFoodSelector);
                BTSequence noticedSequence = new BTSequence("Player Is Noticed", WildNoticePlayerList);
            #endregion
        #endregion
        return noticedSequence;
    }
}
