using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wildNoticeTrigger : MonoBehaviour
{

    public GameObject creature;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(!creature.GetComponent<CreatureAIContext>().isNoticed)
            creature.GetComponent<CreatureAIContext>().isNoticed = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(creature.GetComponent<CreatureAIContext>().isNoticed)
            creature.GetComponent<CreatureAIContext>().isNoticed = false;
        }
    }
}
