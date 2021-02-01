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
            //other.gameObject.GetComponent<PlayerStats>().TakeDamage(context.damage);
            other.gameObject.GetComponent<StatManager>().TakeDamage(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().DeathCheck();
            other.GetComponent<PlayerController>().isHit = true;
        }
    }

    // public void ColliderOnOff()
    // {
    //     hitbox.enabled = !hitbox.enabled;
    // }

    
}
