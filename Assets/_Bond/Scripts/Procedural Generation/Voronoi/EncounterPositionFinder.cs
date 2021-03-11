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

    public List<Vector2> GetPoints(Vector3 startPos, int mapSize, int increment)
    {
        Debug.Log("GetPoints Started");
        timerStart = Time.realtimeSinceStartup;
        List<Vector2> viablePoints = new List<Vector2>(); 
        gameObject.transform.position = startPos;
        Vector3 position = startPos;
        Collider[] hitColliders = new Collider[1];
        int layer = 1<<10;
        for(float x = startPos.x; x < mapSize; x += increment)
        {
            for(float y = startPos.y; y < mapSize; y += increment)
            {
                position = new Vector3(x,37.5f,y);
                int numColliders = Physics.OverlapSphereNonAlloc(position, 23, hitColliders);
                if(numColliders < 1)
                {
                    viablePoints.Add(new Vector2(x,y));
                } else
                {
                }
                // if(Physics.SphereCast(position, 23, Vector3.zero, out hit, 0.1f, layer))
                // {
                //     Debug.Log("Did Hit");
                // } 
                // else
                // {
                //     viablePoints.Add(new Vector2(x,y));
                // }

                // if(!inTerrain)
                // {
                //     viablePoints.Add(new Vector2(x,y));
                // }
            }
        }
        Debug.Log("GetPoints Finished : " + (Time.realtimeSinceStartup - timerStart));
        return viablePoints;
    }
}
