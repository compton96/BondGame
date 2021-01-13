using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    [Serializable]
    public class islandLayer {
        //chance to select any random thing
        //elevated or not?
        //random pool to select one layer
        //public bool isRandom;
        public List<NodeInfo> chunks = new List<NodeInfo>();
    }

    [Serializable]
    public class GenLayerContainer {
        public List<islandLayer> layers;
        public GenLayerContainer() {
            layers = new List<islandLayer>();
        }
    }

    [Serializable]
    public struct Connectors { 
        public Vector2 northConnector; 
        public Vector2 eastConnector; 
        public Vector2 southConnector; 
        public Vector2 westConnector; 
    }

    public Connectors connectors;
    public List<GenLayerContainer> layerContainer = new List<GenLayerContainer>();
    public Vector2 islandSize;
    public float offset;
    public float heightOffset;
    //public List<NodeInfo> nodeInfos = new List<NodeInfo>();



    private void Awake() {

        //process height 
        //process random (event, creature, enemy)
        //generate level!


        //Height Level
        int chunkIndex = 0;
        for (int i = 0; i < islandSize.x; i++) //x
        {
            for(int j = 0; j < islandSize.y; j++) //y
            {
                if(layerContainer[1].layers[0].chunks[chunkIndex].name != "") //if not empty
                {
                    if(true) //some event proved true
                    {
                        //FIX THIS LATER
                        //layerContainer[0].layers[0].chunks[chunkIndex].height = heightOffset;
                        
                    }
                }
                chunkIndex++;
            }
        }

        //process random layer
        chunkIndex = 0;
        int randomLayerType = UnityEngine.Random.Range(2,5);
        //ProcessLayer(randomLayerType);

        //generate level
        //GenerateIsland();
    }

    public void ProcessLayer(int layerType){
        
        int chunkIndex = 0;
        int randomLayer = UnityEngine.Random.Range(0,layerContainer[layerType].layers.Count - 1);
        for (int i = 0; i < islandSize.x; i++) //x
        {
            for(int j = 0; j < islandSize.y; j++) //y
            {
                if(layerContainer[layerType].layers[randomLayer].chunks[chunkIndex].name != "") //if not empty
                {
                    layerContainer[0].layers[0].chunks[chunkIndex] = layerContainer[layerType].layers[randomLayer].chunks[chunkIndex];
                }
                chunkIndex++;
            }
        }
    }

    public void GenerateIsland() 
    {
        int chunkIndex = 0;
        for (int i = 0; i < islandSize.x; i++) 
        {
            for(int j = 0; j < islandSize.y; j++) 
            {
              
                GameObject cube = layerContainer[0].layers[0].chunks[chunkIndex].nodes[UnityEngine.Random.Range(0, layerContainer[0].layers[0].chunks[chunkIndex].nodes.Count)];
                GameObject node = Instantiate(cube, Vector3.zero, Quaternion.identity, gameObject.transform);
                node.transform.localPosition = new Vector3(offset * i, layerContainer[0].layers[0].chunks[chunkIndex].height, offset * j);
                chunkIndex++;
            }
        }
    }
}
