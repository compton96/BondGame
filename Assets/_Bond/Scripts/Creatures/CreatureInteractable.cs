using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractable : InteractableBase
{
    public GameObject Creature;

    protected override void DoInteract()
    {
        var pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        pc.wildCreature = Creature;
        pc.befriendCreature();

    }
}
