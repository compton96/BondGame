using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int mapSize = 3;
    public List<GameObject> islands;
    public GameObject path;


    public struct IslandStorage { 
        public Vector2 position;
        public IslandGenerator IGen;


        public IslandStorage(Vector2 _pos, IslandGenerator _IGen) {
            position = _pos;
            IGen = _IGen;
        }
    }

    public IslandStorage[,] islandStorage;


    private void Awake() {

        //generate base gameobjects
        islandStorage = new IslandStorage[mapSize, mapSize];
        for(int i = 0; i < mapSize; i++) 
        {
            for(int j = 0; j < mapSize; j++) 
            {
                Vector3 pos = new Vector3(100 * i, 0, 100 * j);
                GameObject newIsland = Instantiate(islands[Random.Range(0,islands.Count)], pos, Quaternion.identity);
                islandStorage[i,j] = new IslandStorage(new Vector2(i,j), newIsland.GetComponent<IslandGenerator>()); 
            }
        }

        //pick different area types (creature enemy event)
        // List<int> randomLayers = new List<int>();
        // randomLayers.Add(2);
        // randomLayers.Add(3);
        // randomLayers.Add(4);

        int randomLayerType; 
        for(int i = 0; i < mapSize; i++) 
        {
            for(int j = 0; j < mapSize; j++) 
            {
                randomLayerType = UnityEngine.Random.Range(2,5);
                islandStorage[i,j].IGen.ProcessLayer(randomLayerType);
            }
        }
        
        //generate islands

        for(int i = 0; i < mapSize; i++) 
        {
            for(int j = 0; j < mapSize; j++) 
            {
                islandStorage[i,j].IGen.GenerateIsland();
            }
        }


        for(int i = 0; i < mapSize; i++) 
        {
            for(int j = 0; j < mapSize; j++) 
            {
                //north connection and across 
                Transform parent = islandStorage[i,j].IGen.transform;
                if(j < mapSize - 1){
                    Vector2 northStart = new Vector2(islandStorage[i,j].IGen.connectors.northConnector.x, islandStorage[i,j].IGen.connectors.northConnector.y);
                  
                    //up
                    for(int n = (int) northStart.y; n < islandStorage[i,j].IGen.islandSize.y; n++ ) 
                    {
                        Vector3 pos = new Vector3((i * 100) + northStart.x * 10, 0, (j * 100) + n * 10);
                        var cube = Instantiate(path, pos, Quaternion.identity, parent);
                        cube.name = "north";
                    }
                    //across
                    Vector2 northEnd = new Vector2(islandStorage[i,j + 1].IGen.connectors.southConnector.x, islandStorage[i, j + 1].IGen.connectors.southConnector.y);
                    if(northStart.x != northEnd.x)
                    {
                        if(northStart.x <= northEnd.x) 
                        {
                            for(int n = (int) northStart.x; n <= northEnd.x; n++ ) 
                            {
                                Vector3 pos = new Vector3((i * 100) + n * 10, 0, (j * 100) + 9 * 10);
                                var cube = Instantiate(path, pos, Quaternion.identity, parent);
                                cube.name = "north";
                            }
                        }
                        else 
                        {
                            for(int n = (int) northEnd.x; n <= northStart.x; n++ ) 
                            {
                                Vector3 pos = new Vector3((i * 100) + n * 10, 0, (j * 100) + 9 * 10);
                                var cube = Instantiate(path, pos, Quaternion.identity, parent);
                                cube.name = "north";
                            }
                        }
                    }   
                }

                //south
                if(j > 0){
                    Vector2 southStart = new Vector2(islandStorage[i,j].IGen.connectors.southConnector.x, islandStorage[i,j].IGen.connectors.southConnector.y);
                   
                    //down
                    for(int n = (int) southStart.y; n >= 0; n-- ) 
                    {
                        Vector3 pos = new Vector3((i * 100) + southStart.x * 10, 0, (j * 100) + n * 10);
                        var cube = Instantiate(path, pos, Quaternion.identity, parent);
                        cube.name = "south";
                    }
                   
                }
                

                //east connection and across 
                if(i < mapSize - 1){
                    Vector2 eastStart = new Vector2(islandStorage[i,j].IGen.connectors.eastConnector.x, islandStorage[i,j].IGen.connectors.eastConnector.y);
                  
                    //right
                    for(int n = (int) eastStart.x; n < islandStorage[i,j].IGen.islandSize.x; n++ ) 
                    {
                        Vector3 pos = new Vector3((i * 100) + n * 10, 0, (j * 100) + eastStart.y * 10);
                        var cube = Instantiate(path, pos, Quaternion.identity, parent);
                        cube.name = "east";
                    }
                    //up/down
                    Vector2 eastEnd = new Vector2(islandStorage[i + 1,j].IGen.connectors.westConnector.x, islandStorage[i + 1, j].IGen.connectors.westConnector.y);
                    if(eastStart.y != eastEnd.y)
                    {
                        if(eastStart.y <= eastEnd.y) 
                        {
                            for(int n = (int) eastStart.y; n <= eastEnd.y; n++ ) 
                            {
                                Vector3 pos = new Vector3((i * 100) + 9 * 10, 0, (j * 100) + n * 10);
                                var cube = Instantiate(path, pos, Quaternion.identity, parent);
                                cube.name = "east";
                            }
                        }
                        else 
                        {
                            for(int n = (int) eastEnd.y; n <= eastStart.y; n++ ) 
                            {
                                Vector3 pos = new Vector3((i * 100) + 9 * 10, 0, (j * 100) + n * 10);
                                var cube = Instantiate(path, pos, Quaternion.identity, parent);
                                cube.name = "east";
                            }
                        }
                    }   
                }

                //west and across 
                if(i > 0){
                    Vector2 westStart = new Vector2(islandStorage[i,j].IGen.connectors.westConnector.x, islandStorage[i,j].IGen.connectors.westConnector.y);
                   
                    //left
                    for(int n = (int) westStart.x; n >= 0; n-- ) 
                    {
                        Vector3 pos = new Vector3((i * 100) + n * 10, 0, (j * 100) + westStart.y * 10);
                        var cube = Instantiate(path, pos, Quaternion.identity, parent);
                        cube.name = "west";
                    }
                   
                }
            }
        }
        

    }
}
