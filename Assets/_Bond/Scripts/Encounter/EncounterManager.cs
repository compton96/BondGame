using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public int currEnemyCount = 0;
    public GameObject barrier;
    
    private int currWave = 0;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            barrier.SetActive(true);
            SpawnEncounter();
            GetComponent<Collider>().enabled = false;
            PersistentData.Instance.Player.GetComponent<PlayerController>().InCombat(true);
        }
    }
    
    public void SpawnEncounter()
    {
        if(waves[currWave].spawnWholeWave)
        {
            foreach(GameObject spawner in waves[currWave].spawners)
            {
                spawner.GetComponent<EnemySpawner>().spawnEnemy(this);
                currEnemyCount++;
            }
            currWave++;
        } 
        else 
        {
            spawnNextEnemy();
        }


    }

    public void enemyKilled()
    {
        currEnemyCount--;
        if(currWave >= waves.Count)
        {
            //clear encounter
            if(currEnemyCount < 1)
            {
                barrier.SetActive(false);
                PersistentData.Instance.Player.GetComponent<PlayerController>().InCombat(false);
                return;
            }
        }
        if(currEnemyCount <= waves[currWave].waitUntilEnemiesLeft)
        {
            if(waves[currWave].spawnWholeWave)
            {
                foreach(GameObject spawner in waves[currWave].spawners)
                {
                    spawner.GetComponent<EnemySpawner>().spawnEnemy(this);
                    currEnemyCount++;
                }
                currWave++;
            } else {
                spawnNextEnemy();
            }
            
        }
    }

    public void spawnNextEnemy()
    {

        if(waves[currWave].index < waves[currWave].spawners.Count)
        {
            waves[currWave].spawners[waves[currWave].index].GetComponent<EnemySpawner>().spawnEnemy(this);
            currEnemyCount++;
            waves[currWave].index++;
        } 
        else
        {
            if(currWave < waves.Count)
            {
                currWave++;
                spawnNextEnemy();
                
            }
            else 
            {
                if(currEnemyCount < 1)
                {
                    barrier.SetActive(false);
                    PersistentData.Instance.Player.GetComponent<PlayerController>().InCombat(false);
                }
                
            }
        }
    }
}



[System.Serializable]
public class Wave 
{
    public bool spawnWholeWave;
    public int waitUntilEnemiesLeft = 0; //if 0 it will wait for last wave to be finished; otherwise it will spawn more enemies as you kill them;
    public int index = 0;
    public List<GameObject> spawners = new List<GameObject>();
}