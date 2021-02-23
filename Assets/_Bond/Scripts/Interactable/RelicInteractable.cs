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
            ApplyModifiers(PersistentData.Instance.Player.GetComponent<PlayerController>().stats);
            Destroy(gameObject);
        }
        
    }

    public void ApplyModifiers(StatManager _statManager)
    {
        Debug.Log("Applying modifiers");
        _statManager.AddRelic(relicStats);
    }
}
