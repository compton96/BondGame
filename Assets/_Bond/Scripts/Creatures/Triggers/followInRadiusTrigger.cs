using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followInRadiusTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "CaptCreature") 
        {
            if(!other.GetComponent<CreatureAIContext>().isInPlayerRadius)
            other.GetComponent<CreatureAIContext>().isInPlayerRadius = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "CaptCreature") 
        {
            if(other.GetComponent<CreatureAIContext>().isInPlayerRadius)
            other.GetComponent<CreatureAIContext>().isInPlayerRadius = false;
        }
    }
}
