using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractRadius : MonoBehaviour
{
    public PlayerController pc;

    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log(other.transform.tag);
        
        if(other.transform.tag == "Interactable")
        {
            pc.interactableObjects.Add(other.gameObject, other.gameObject.GetComponent<InteractableBase>());
            if( other.gameObject.GetComponent<InteractableBase>().showUI)
            {
                PersistentData.Instance.UI.GetComponent<UIUpdates>().showInteractPrompt();
            }
            
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Interactable")
        {
            pc.interactableObjects.Remove(other.gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.UI.GetComponent<UIUpdates>().hideIntereactPrompt();
            }
            
            if(other.gameObject.layer == 13)
            {
                PersistentData.Instance.UI.GetComponent<UIUpdates>().HideCharacterDialogue();
                pc.characterDialogManager = null;
                pc.inCharacterDialog = false;
            }
        }
    }
}
