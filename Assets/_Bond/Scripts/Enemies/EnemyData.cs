﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;

    public float maxLife;
    public int damage;
    public float moveSpeed;
    
    public void DestroyEnemy()
    {
        Destroy(enemyPrefab);
    }
}
