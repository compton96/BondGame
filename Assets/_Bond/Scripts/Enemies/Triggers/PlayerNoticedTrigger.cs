using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoticedTrigger : MonoBehaviour
{
    public GameObject enemy;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(!enemy.GetComponent<EnemyAIContext>().playerNoticed)
            enemy.GetComponent<EnemyAIContext>().playerNoticed = true;
            enemy.GetComponent<EnemyAIContext>().playerNoticedBefore = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player") 
        {
            if(enemy.GetComponent<EnemyAIContext>().playerNoticed)
            enemy.GetComponent<EnemyAIContext>().playerNoticed = false;
        }
    }
}
