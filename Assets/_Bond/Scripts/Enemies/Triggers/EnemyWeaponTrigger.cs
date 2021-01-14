using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyAIContext context;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().takeDamage(context.damage);
            other.GetComponent<PlayerController>().isHit = true;
        }
    }
}
