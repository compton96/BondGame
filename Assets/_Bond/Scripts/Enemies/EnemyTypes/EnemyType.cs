// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyType", order = 1)]
public class EnemyType : ScriptableObject
{
    public string enemyName; //Enemy name

    public BTEnemySubtree attack;
    public BTEnemySubtree idle;

}
