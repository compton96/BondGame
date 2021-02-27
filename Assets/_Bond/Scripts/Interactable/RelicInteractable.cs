using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInteractable : InteractableBase
{
    public RelicStats relicStats;
    public int cost = 1;

    private void Awake() 
    {
        showUI = true;
        removeOnInteract = false;
    }  

    public override void DoInteract()
    {
        if(PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount >= cost)
        {
            PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount -= cost;
            removeOnInteract = true;
            ApplyModifiers();
            Destroy(gameObject);
        }
        
    }

    public void ApplyModifiers()
    {
        var pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        Debug.Log("Applying modifiers");
        pc.stats.AddRelic(relicStats.playerModifiers);
        if(pc.currCreature != null)
        {
            pc.currCreatureContext.creatureStats.statManager.AddRelic(relicStats.creatureModifiers);
        }

        if(pc.swapCreature != null)
        {
            pc.swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager.AddRelic(relicStats.creatureModifiers);
        }


        pc.Relics.Add(relicStats);

    }
}
