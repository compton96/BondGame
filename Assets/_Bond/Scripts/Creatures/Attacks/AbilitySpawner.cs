using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{

    public void SpawnProjectile(GameObject projectile, GameObject target, float speed, float damage, bool isHoming) 
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<ProjectileScript>().setTarget(target, speed, damage, isHoming);
    }

    public void SpawnSunBeam(GameObject projectile, GameObject target, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, target.transform.position, Quaternion.identity);
        proj.GetComponent<SunBeam>().setDamage(damage, debuff);
    }

    public void SpawnSporeToss(GameObject projectile, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        proj.GetComponent<SporeToss>().setDamage(damage, debuff);
    }

}
