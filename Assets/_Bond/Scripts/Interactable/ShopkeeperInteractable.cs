using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperInteractable : InteractableBase
{
    CharacterDialogManager dialogManager => GetComponent<CharacterDialogManager>(); 

    private void Awake() {
        showUI = true;
        removeOnInteract = true;
    }

    public override void DoInteract()
    {
        PersistentData.Instance.Player.GetComponent<PlayerController>().characterDialogManager = dialogManager;
        dialogManager.StartConvo();
    }
}
