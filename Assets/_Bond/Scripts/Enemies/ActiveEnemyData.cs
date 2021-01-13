// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyData : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float maxLife;
    public float currentHealth;
    public int damage;
    public float moveSpeed;
    // public List<creatureAttackBase> creatureAttacks;

    public void takeDamage(float amount){
        // Debug.Log("Took " + amount + " damage");
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(enemyPrefab);
    }
}
