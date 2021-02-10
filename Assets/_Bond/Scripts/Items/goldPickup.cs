using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.goldCount++;
            Destroy(gameObject);
        }
    }
}
