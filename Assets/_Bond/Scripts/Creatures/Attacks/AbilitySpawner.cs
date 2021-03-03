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

    public void SpawnPetals(GameObject projectile, Transform creatureTransform) 
    {
        Debug.Log("Creature Rot: " + creatureTransform.eulerAngles.y);
        var creatureRotation = creatureTransform.rotation;
        creatureRotation *= Quaternion.Euler(0, -90, 0);
        Quaternion rot = new Quaternion(creatureTransform.rotation.x, creatureTransform.rotation.y, creatureTransform.rotation.z, creatureTransform.rotation.w);
        var proj = Instantiate(projectile, gameObject.transform.position, creatureRotation);
        // proj.GetComponent<PetalThrow>().setTarget(target, speed, damage, isHoming);
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

    public void SpawnWaterBeam(GameObject projectile, GameObject target, float speed, float damage, bool isHoming, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<WaterBeam>().setDamage(damage, debuff);
        proj.GetComponent<WaterBeam>().setTarget(target, speed, damage, isHoming);
    }

}
