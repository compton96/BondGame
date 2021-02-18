using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBeam : MonoBehaviour
{
    private float damage;

    private void Awake() 
    {
        Destroy(gameObject, 5f);
    }

    public void setDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyAIContext>().statManager.TakeDamage(damage, ModiferType.RANGED_RESISTANCE);
            Destroy(gameObject);
        }    
    }
}
