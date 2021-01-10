using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
 // The GameObject to instantiate.

    // An instance of the ScriptableObject defined above.
    public Item item;
    private GameObject playerObj = null;

    void Start()
    {
        if (playerObj == null)
        {
             playerObj = GameObject.Find("Player");
        }
        SpawnEntities();
    }

    void SpawnEntities()
    {
        // int currentSpawnPointIndex = 0;
        // Creates an instance of the prefab at the current spawn point.
        GameObject currentEntity = Instantiate(item.prefab, item.spawnPoints, Quaternion.identity);

        // Sets the name of the instantiated entity to be the string defined in the ScriptableObject. 
        currentEntity.name = item.itemName;
        // currentEntity.CreatureFood = item.CreatureFood;

        // currentSpawnPointIndex = (currentSpawnPointIndex + 1) % item.spawnPoints.Length;
    }
}