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
            {
                other.GetComponent<CreatureAIContext>().isInPlayerRadius = true;
            }
        } else if(other.transform.tag == "Enemy") 
        {
            if(!other.GetComponent<EnemyAIContext>().isInPlayerRadius)
            {
                other.GetComponent<EnemyAIContext>().isInPlayerRadius = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "CaptCreature") 
        {
            if(other.GetComponent<CreatureAIContext>().isInPlayerRadius)
            {
                other.GetComponent<CreatureAIContext>().isInPlayerRadius = false;
            }
        } else if(other.transform.tag == "Enemy") 
        {
            if(other.GetComponent<EnemyAIContext>().isInPlayerRadius)
            {
                other.GetComponent<EnemyAIContext>().isInPlayerRadius = false;
            }
        }
    }
}
