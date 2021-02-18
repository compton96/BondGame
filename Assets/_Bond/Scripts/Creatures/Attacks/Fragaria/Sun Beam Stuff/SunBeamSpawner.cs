using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBeamSpawner : MonoBehaviour
{
    public void SpawnSunBeam(GameObject projectile, GameObject target, float damage)
    {
        var proj = Instantiate(projectile, target.transform.position, Quaternion.identity);
        proj.GetComponent<SunBeam>().setDamage(damage);
    }
}
