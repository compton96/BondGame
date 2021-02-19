using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBeam : MonoBehaviour
{
    private float damage;
    private Buff debuff;

    private void Awake() 
    {
        Destroy(gameObject, 2f);
    }

    public void setDamage(float _damage, Buff _debuff)
    {
        damage = _damage;
        debuff = _debuff;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Enemy")
        {
            //Add debuff to enemy
            other.transform.GetComponent<EnemyAIContext>().statManager.AddBuff(debuff);
        }    
    }
}
