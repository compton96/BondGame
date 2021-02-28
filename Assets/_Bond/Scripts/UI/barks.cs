using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class barks : MonoBehaviour
{
    public List<string> barkList = new List<string>();
    public TextMeshProUGUI barkBox;
    
    private bool doBarks;
    private float timer = 0;
    private float showTimer = 10;
    private float textFadeTimer = 5;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            // Debug.Log("Do Barks");
            doBarks = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player")
        {
            // Debug.Log("Dont Barks");
            doBarks = false;
        }
    }
    
    private void Update() {

        timer+= Time.deltaTime;
        if(doBarks)
        {
            barkBox.enabled = true;
            if(timer >= textFadeTimer)
            {
               barkBox.text = "";
            }
            if(timer >= showTimer)
            {
                barkBox.text = barkList[Random.Range(0,barkList.Count)];
                timer = 0;
            }
        } 
        else
        {
            barkBox.enabled = false;
        } 
    }
}
