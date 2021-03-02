using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> Enemies = new List<GameObject>();

    public void SpawnEnemy(EncounterManager _manager)
    {
        
        int index = Enemies.Count > 1 ? Random.Range(0,Enemies.Count) : 0;
        GameObject enemy = Instantiate(Enemies[index], gameObject.transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyAIContext>().EncounterManager = _manager;
    }
}
