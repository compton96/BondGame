//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Creature Melee Attack", menuName = "ScriptableObjects/CreatureAttacks/MeleeAttack", order = 2)]
public class creatureAttackMelee : creatureAttackBase
{
    public float maxDistanceToEnemy;
    public float moveSpeed;
    public Animation anims;
    public string animTrigger;
    public float baseDmg;
    public float hitRadius;
}
