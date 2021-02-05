using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    public RelicStats relicStats;

    public void applyModifiers(StatManager _statManager)
    {
        _statManager.AddRelic(relicStats);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().nearInteractable = true;
            other.GetComponent<PlayerController>().interactableObject = gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().nearInteractable = false;
            other.GetComponent<PlayerController>().interactableObject = null;
        }
    }
}
