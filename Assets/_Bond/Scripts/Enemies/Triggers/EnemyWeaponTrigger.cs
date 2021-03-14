using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyAIContext context;

    //public BoxCollider hitbox => gameObject.GetComponent<BoxCollider>();

    [FMODUnity.EventRef]
    public string playerHitSlashSFX;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatManager>().TakeDamage(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().DeathCheck();
            other.GetComponent<PlayerController>().isHit = true;

            FMODUnity.RuntimeManager.PlayOneShot(playerHitSlashSFX, transform.position);
        }
        else if(other.gameObject.tag == "CaptCreature")
        {
            other.gameObject.GetComponent<StatManager>().TakeDamageCreature(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.GetComponent<CreatureAIContext>().isHit = true;
            PersistentData.Instance.UI.GetComponent<UIUpdates>().updateEnthusiasm();
        }
    }

    // public void ColliderOnOff()
    // {
    //     hitbox.enabled = !hitbox.enabled;
    // }

    
}
