﻿//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Creature Ranged Attack", menuName = "ScriptableObjects/CreatureAttacks/RangedAttack", order = 1)]
public class creatureAttackRanged : creatureAttackBase, HasCooldown
{
    public float maxDistanceToEnemy;
    public float projectileSpeed;
    public GameObject projectile;
    public Animation anims;
    public float baseDmg;
    public bool isHoming;
    public float speed;
    public float damage;
    public GameObject target;
    private void Awake() 
    {
        cooldownDuration = 2f;
    }
    
    new public int Id => id;
    
    new public float CooldownDuration => cooldownDuration;
    
}
