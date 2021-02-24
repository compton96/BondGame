using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractable : InteractableBase
{
    public GameObject Creature;

    private void Awake() 
    {
        showUI = true;
        removeOnInteract = true;
    }    

    public override void DoInteract()
    {
        if(Creature.GetComponent<CreatureAIContext>().isWild)
        {
            DoInteractWild();
        } else 
        {
            DoInteractTamed();
        }
    }

    public void DoInteractWild()
    {
        var pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        pc.wildCreature = Creature;
        pc.befriendCreature();
        pc.wildCreature = null;
        showUI = false;

        gameObject.SetActive(false); //THIS IS TEMPORARY

    }

    public void DoInteractTamed()
    {
        //do hug animation
    }
}
