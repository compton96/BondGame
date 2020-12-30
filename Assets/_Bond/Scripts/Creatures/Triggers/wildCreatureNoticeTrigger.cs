using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildCreatureNoticeTrigger : MonoBehaviour
{
    public wildCreature wildCreature;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            wildCreature.playerInRadius = true;
            Debug.Log("player in radius");
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            wildCreature.playerInRadius = false;
            Debug.Log("player left radius");
        }
    }
}
