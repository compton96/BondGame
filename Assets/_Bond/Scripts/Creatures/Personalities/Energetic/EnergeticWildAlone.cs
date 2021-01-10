//Eugene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergeticWildAlone", menuName = "ScriptableObjects/BTSubtrees/Energetic/WildAlone")]
public class EnergeticWildAlone : BTSubtree
{
    public override BTSelector BuildSelectorSubtree(CreatureAIContext context) 
    {
        #region WILD NO PLAYER
            List<BTNode> WildNoPlayerList = new List<BTNode>();

            #region wild enemies nearby
                List<BTNode> WildEnemiesList = new List<BTNode>();
                CCheckWildEnemiesInRange wildEnemies = new CCheckWildEnemiesInRange("ENERGETIC Are Enemies Nearby?", context);
                CActionEnergeticWildRunFromEnemies wildRunEnemy = new CActionEnergeticWildRunFromEnemies("ENERGETIC Run From Enemies", context);
                WildEnemiesList.Add(wildEnemies);
                WildEnemiesList.Add(wildRunEnemy);
                BTSequence wildEnemySequence = new BTSequence("ENERGETIC Wild Enemies Nearby", WildEnemiesList);
            #endregion

            #region wild wander
                List<BTNode> WildWanderList = new List<BTNode>();
                CActionEnergeticWildWanderInLocation wildWander = new CActionEnergeticWildWanderInLocation("ENERGETIC Wander", context);
                CActionEnergeticWildWanderIdle wildWanderIdle = new CActionEnergeticWildWanderIdle("ENERGETIC Wander Idle", context);
                WildWanderList.Add(wildWander);
                WildWanderList.Add(wildWanderIdle);
                BTSelector wildWanderSequence = new BTSelector("ENERGETIC Wild Wander", WildWanderList);
            #endregion

            WildNoPlayerList.Add(wildEnemySequence);
            WildNoPlayerList.Add(wildWanderSequence);
            BTSelector wildNoPlayerSelector = new BTSelector("ENERGETIC Wild No Player", WildNoPlayerList);
        #endregion
        return wildNoPlayerSelector;
    }
}
