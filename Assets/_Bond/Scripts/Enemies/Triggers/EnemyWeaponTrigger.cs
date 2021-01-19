using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyAIContext context;

    //public BoxCollider hitbox => gameObject.GetComponent<BoxCollider>();

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().takeDamage(context.damage);
            other.GetComponent<PlayerController>().isHit = true;
        }
    }

    // public void ColliderOnOff()
    // {
    //     hitbox.enabled = !hitbox.enabled;
    // }

    
}
