using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractable : MonoBehaviour
{

    public Collider interactRadius;
    public GameObject Creature;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            var pc = other.GetComponent<PlayerController>();
            pc.nearInteractable = true;
            pc.wildCreature = Creature;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player") {
            var pc = other.GetComponent<PlayerController>();
            pc.nearInteractable = false;
            pc.wildCreature = null;
        }
    }
}
