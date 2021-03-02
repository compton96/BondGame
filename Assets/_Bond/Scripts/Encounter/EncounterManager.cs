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
            PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusic();
        }
    }
    
    public void SpawnEncounter()
    {
        if(waves[currWave].spawnWholeWave)
        {
            foreach(GameObject spawner in waves[currWave].spawners)
            {
                SpawnNextEnemy();
            }
        } 
        else 
        {
            SpawnNextEnemy();
        }


    }

    public void enemyKilled()
    {
        currEnemyCount--;
        if(currWave < waves.Count)
        {
            if(waves[currWave].index < waves[currWave].spawners.Count)
            {
                SpawnNextEnemy();
            } 
            else 
            {
                currWave++;
                if(currWave < waves.Count)
                {
                    if(waves[currWave].spawnWholeWave)
                    {
                        foreach(GameObject spawner in waves[currWave].spawners)
                        {
                            SpawnNextEnemy();
                        }
                    }
                    else
                    {
                        SpawnNextEnemy();
                    }
                }
            }
        }
        else
        {
            if(currEnemyCount < 1)
            {
                ClearEncounter();
            }
        }





       
    }

    public void SpawnNextEnemy()
    {
        waves[currWave].spawners[waves[currWave].index].GetComponent<EnemySpawner>().SpawnEnemy(this);
        currEnemyCount++;
        waves[currWave].index++;
    }


    private void ClearEncounter()
    {
        barrier.SetActive(false);
        PersistentData.Instance.Player.GetComponent<PlayerController>().InCombat(false);
        PersistentData.Instance.AudioController.GetComponent<AudioController>().EndCombatMusic();
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