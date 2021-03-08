using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionBlob : MonoBehaviour
{
    public Buff corruptionEffect;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().stats.AddBuff(corruptionEffect);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().stats.RemoveBuff(corruptionEffect);
        }
    }
}
