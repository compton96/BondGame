// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrawnyWildAlone", menuName = "ScriptableObjects/BTSubtrees/Scrawny/WildAlone")]
public class ScrawnyWildAlone : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region WILD NO PLAYER
            List<BTNode> WildNoPlayerList = new List<BTNode>();

            #region wild enemies nearby
                List<BTNode> WildEnemiesList = new List<BTNode>();
                CCheckWildEnemiesInRange wildEnemies = new CCheckWildEnemiesInRange("Are Enemies Nearby?", context);
                CActionWildRunFromEnemies wildRunFromEnemy = new CActionWildRunFromEnemies("Run From Enemies", context);
                WildEnemiesList.Add(wildEnemies);
                WildEnemiesList.Add(wildRunFromEnemy);
                BTSequence wildEnemySequence = new BTSequence("Wild Enemies Nearby", WildEnemiesList);
            #endregion

            #region Wild Find Food
                List<BTNode> FindFoodList = new List<BTNode>();
                CCheckHeartyWildDroppedFood findFood = new CCheckHeartyWildDroppedFood("Is food nearby?", context);
                CActionHeartyWildApproachDroppedFood getFood = new CActionHeartyWildApproachDroppedFood("Go Eat food", context);
                FindFoodList.Add(findFood);
                FindFoodList.Add(getFood);
                BTSequence findFoodSequence = new BTSequence("Wild Find Food", FindFoodList);
            #endregion

            #region wild wander
                List<BTNode> WildWanderList = new List<BTNode>();
                CActionWildWanderInLocation wildWander = new CActionWildWanderInLocation("Wander", context);
                CActionWildWanderIdle wildWanderIdle = new CActionWildWanderIdle("Wander Idle", context);
                WildWanderList.Add(findFoodSequence);
                WildWanderList.Add(wildWander);
                WildWanderList.Add(wildWanderIdle);
                BTSelector wildWanderSelector = new BTSelector("Wild Wander", WildWanderList);
            #endregion

            WildNoPlayerList.Add(wildEnemySequence);
            WildNoPlayerList.Add(wildWanderSelector);
            BTSelector wildNoPlayerSelector = new BTSelector("Wild No Player", WildNoPlayerList);
        #endregion
        return wildNoPlayerSelector;
    }
}
