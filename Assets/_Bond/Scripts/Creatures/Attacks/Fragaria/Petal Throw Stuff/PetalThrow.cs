using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalThrow : MonoBehaviour
{
    private CreatureAIContext context;

    private void Awake() 
    {
        context=PersistentData.Instance.Player.GetComponent<PlayerController>().currCreatureContext;
        Debug.Log(context);
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Enemy")
        {
            if(context.enemyList == null)
            {
                context.enemyList = new List<GameObject>();
                
            }
            Debug.Log(context.enemyList);
            context.enemyList.Add(other.transform.gameObject);
        }    
    }
}
