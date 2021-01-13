using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesigner : MonoBehaviour
{
    public Vector2 islandSize;
    public float offset;
    public GameObject NodePlaceHolder;
    public NodeInfo emtpyInfo;
    public List<NodeInfo> nodesTypes = new List<NodeInfo>();
    public GameObject button;
    public GameObject UICanvas;
    private GameObject Level;
    public IslandGenerator IGen; 
    public int currentLayerType;
    
    

    [Serializable]
    public class DesignerLayerContainer {
        public int currentLayer; 
        public List<GameObject> layerParents;
        public Vector3 position;
        public DesignerLayerContainer(GameObject parent) {
            currentLayer = 0;
            layerParents = new List<GameObject>();
            layerParents.Add(parent);
            position = new Vector3(0,0,0);
        }
    }

    public List<DesignerLayerContainer> Layers = new List<DesignerLayerContainer>();
    //ui access
    



    private void Start()
    {
        Level = new GameObject("Level");
        Level.transform.position = Vector3.zero;
        IGen = Level.AddComponent<IslandGenerator>();
        IGen.islandSize = islandSize;
        IGen.offset = offset;
        
        for(int b = 0; b < nodesTypes.Count; b ++) 
        {
            GameObject temp = Instantiate(button, Vector3.zero, Quaternion.identity, UICanvas.transform);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(-375, 190 -(b * 30), 0);
            temp.GetComponent<levelUIButtonHelper>().setupButton(nodesTypes[b]);
        }

        //init layers
        IGen.layerContainer.Add(new IslandGenerator.GenLayerContainer());
        IGen.layerContainer.Add(new IslandGenerator.GenLayerContainer());
        IGen.layerContainer.Add(new IslandGenerator.GenLayerContainer());
        IGen.layerContainer.Add(new IslandGenerator.GenLayerContainer());
        IGen.layerContainer.Add(new IslandGenerator.GenLayerContainer());

        
        Layers.Add(new DesignerLayerContainer(createLayer(0, new Vector3(-7, 0 , 21))));
        Layers[0].layerParents[0].SetActive(true);
        Layers.Add(new DesignerLayerContainer(createLayer(1, new Vector3(54, 0, 21))));
        Layers[1].layerParents[0].SetActive(true);

        currentLayerType = 1;

        Layers.Add(new DesignerLayerContainer(createLayer(2, new Vector3(54, 0, 21))));
        Layers[2].layerParents[0].SetActive(false);
        Layers.Add(new DesignerLayerContainer(createLayer(3, new Vector3(54, 0, 21))));
        Layers[3].layerParents[0].SetActive(false);
        Layers.Add(new DesignerLayerContainer(createLayer(4, new Vector3(54, 0, 21))));
        Layers[4].layerParents[0].SetActive(false);
        
        //layers.baseLayer = createLayer(0);
        //layers.baseLayer.SetActive(true);
    }


    public GameObject createLayer(int currentLayer, Vector3 startingPosition) {
        //create new empty game object;
        GameObject layerParent = new GameObject("Layer " + 0);
        layerParent.transform.parent = gameObject.transform;

        IGen.layerContainer[currentLayer].layers.Add(new IslandGenerator.islandLayer());
        int layerLevel = IGen.layerContainer[currentLayer].layers.Count;
        int count = 0;
        for (int i = 0; i < islandSize.x; i++) 
        {
            for(int j = 0; j < islandSize.y; j++) 
            {
                Vector3 pos = new Vector3(offset * i + i * 0.5f, layerLevel * 20, offset * j + 5 + j * 0.5f) + startingPosition;
                GameObject node = Instantiate(
                        NodePlaceHolder, 
                        pos,
                        Quaternion.identity, 
                        layerParent.transform);
                IGen.layerContainer[currentLayer]
                        .layers[IGen.layerContainer[currentLayer]
                        .layers.Count-1]
                        .chunks.Add(emtpyInfo);
                
                node.GetComponent<NodePH>().pos = new Vector2(i,j);
                node.GetComponent<NodePH>().index = count;
                node.GetComponent<NodePH>().LevelDesigner = this;
                node.GetComponent<NodePH>().myLayer = currentLayer;
                
                count++;
            }
        }
        layerParent.SetActive(false);
        return layerParent;
    }



    public void updateNode(int node, NodeInfo newInfo, int _layer) {
        Debug.Log("layer type : " + _layer + " current layer: " + Layers[_layer].currentLayer);
        IGen.layerContainer[_layer]
                .layers[Layers[_layer].currentLayer]
                .chunks[node] = newInfo;
    }
}


