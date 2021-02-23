using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<wave> waves = new List<wave>();
    public int currEnemyCount = 0;
    
    private int currWave = 0;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player")
        {
            
        }
    }
    
    public void SpawnEncounter()
    {
        foreach(GameObject spawner in waves[currWave].spawners)
        {
            spawner.GetComponent<EnemySpawner>().spawnEnemy(this);
            currEnemyCount++;
        }
        currWave++;

    }

    public void enemyKilled()
    {
        currEnemyCount--;
        if(currWave >= waves.Count)
        {
            //clear encounter
            return;
        }
        if(currEnemyCount <= waves[currWave].waitUntilEnemiesLeft)
        {
            spawnNextEnemy();
        }
    }

    public void spawnNextEnemy()
    {
        if(waves[currWave].index < waves[currWave].spawners.Count)
        {
            waves[currWave].spawners[waves[currWave].index].GetComponent<EnemySpawner>().spawnEnemy(this);
            waves[currWave].index++;
        } 
        else
        {
            if(currWave < waves.Count)
            {
                currWave++;
                spawnNextEnemy();
                currEnemyCount++;
            }
            else 
            {
                //clear encounter
            }
        }
    }
}



[System.Serializable]
public class wave 
{
    public int waitUntilEnemiesLeft = 0; //if 0 it will wait for last wave to be finished; otherwise it will spawn more enemies as you kill them;
    public int index = 0;
    public List<GameObject> spawners = new List<GameObject>();
}