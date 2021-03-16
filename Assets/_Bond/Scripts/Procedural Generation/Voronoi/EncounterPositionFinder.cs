using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPositionFinder : MonoBehaviour
{
    bool inTerrain;
    float timerStart;

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Terrain")
        {
            Debug.Log("IN TERRAIN");
            inTerrain = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Terrain")
        {
            inTerrain = false;
        }
    }

    public List<List<Vector2>> GetPoints(Vector3 startPos, int mapSize, int increment)
    {
        Debug.Log("GetPoints Started");
        timerStart = Time.realtimeSinceStartup;
        List<List<Vector2>> listOfLists = new List<List<Vector2>>();
        List<Vector2> viableCombatPoints = new List<Vector2>();
        List<Vector2> viableObjectPoints = new List<Vector2>();
        
        gameObject.transform.position = startPos;
        Vector3 position = startPos;
        Collider[] bigHitColliders = new Collider[1];
        Collider[] smallHitColliders = new Collider[1];
        int layer = 1<<10;
        for(float x = startPos.x; x < mapSize; x += increment)
        {
            for(float y = startPos.y; y < mapSize; y += increment)
            {
                position = new Vector3(x,37.5f,y);
                int bigNumColliders = Physics.OverlapSphereNonAlloc(position, 23, bigHitColliders);
                if(bigNumColliders < 1)
                {
                    viableCombatPoints.Add(new Vector2(x,y));
                } 
                
                position = new Vector3(x,60f,y);
                int smallNumColliders = Physics.OverlapSphereNonAlloc(position, 3, smallHitColliders);
                if(smallNumColliders < 1)
                {
                    viableObjectPoints.Add(new Vector2(x,y));
                } 
                
            }
        }
        listOfLists.Add(viableCombatPoints);
        listOfLists.Add(viableObjectPoints);
        Debug.Log("GetPoints Finished : " + (Time.realtimeSinceStartup - timerStart));
        return listOfLists;
    }
}
