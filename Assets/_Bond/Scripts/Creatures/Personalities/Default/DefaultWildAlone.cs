using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultWildAlone", menuName = "ScriptableObjects/BTSubtrees/Default/WildAlone")]
public class DefaultWildAlone : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region WILD NO PLAYER
            List<BTNode> WildNoPlayerList = new List<BTNode>();

            #region wild enemies nearby
                List<BTNode> WildEnemiesList = new List<BTNode>();
                CCheckWildEnemiesInRange wildEnemies = new CCheckWildEnemiesInRange("Are Enemies Nearby?", context);
                CActionWildRunFromEnemies wildRunEnemy = new CActionWildRunFromEnemies("Run From Enemies", context);
                WildEnemiesList.Add(wildEnemies);
                WildEnemiesList.Add(wildRunEnemy);
                BTSequence wildEnemySequence = new BTSequence("Wild Enemies Nearby", WildEnemiesList);
            #endregion

            #region wild wander
                List<BTNode> WildWanderList = new List<BTNode>();
                CActionWildWanderInLocation wildWander = new CActionWildWanderInLocation("Wander", context);
                CActionWildWanderIdle wildWanderIdle = new CActionWildWanderIdle("Wander Idle", context);
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
