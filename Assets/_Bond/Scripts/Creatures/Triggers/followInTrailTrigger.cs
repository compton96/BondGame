using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followInTrailTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CaptCreature") 
        {
            if(!other.GetComponent<CreatureAIContext>().isInPlayerTrail)
            other.GetComponent<CreatureAIContext>().isInPlayerTrail = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "CaptCreature") 
        {
            if(other.GetComponent<CreatureAIContext>().isInPlayerTrail)
            other.GetComponent<CreatureAIContext>().isInPlayerTrail = false;
        }
    }
}
