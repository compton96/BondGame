// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdventurousWildAlone", menuName = "ScriptableObjects/BTSubtrees/Adventurous/WildAlone")]
public class AdventurousWildAlone : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region WILD NO PLAYER
            List<BTNode> wildNoPlayerList = new List<BTNode>();

            #region wild enemies nearby
                List<BTNode> wildEnemiesList = new List<BTNode>();
                CCheckAdventurousWildEnemiesInRange wildEnemies = new CCheckAdventurousWildEnemiesInRange("Are Enemies Nearby?", context);
                CActionWildRunFromEnemies wildRunEnemy = new CActionWildRunFromEnemies("Run From Enemies", context);
                wildEnemiesList.Add(wildEnemies);
                wildEnemiesList.Add(wildRunEnemy);
                BTSequence wildEnemySequence = new BTSequence("Wild Enemies Nearby", wildEnemiesList);
            #endregion

            #region wild wander
                List<BTNode> wildWanderList = new List<BTNode>();
                CActionAdventurousWildWanderInLocation wildWander = new CActionAdventurousWildWanderInLocation("Wander", context);
                CActionWildWanderIdle wildWanderIdle = new CActionWildWanderIdle("Wander Idle", context);
                wildWanderList.Add(wildWander);
                wildWanderList.Add(wildWanderIdle);
                BTSelector wildWanderSelector = new BTSelector("Wild Wander", wildWanderList);
            #endregion

            wildNoPlayerList.Add(wildEnemySequence);
            wildNoPlayerList.Add(wildWanderSelector);
            BTSelector wildNoPlayerSelector = new BTSelector("Wild No Player", wildNoPlayerList);
        #endregion
        return wildNoPlayerSelector;
    }
}
