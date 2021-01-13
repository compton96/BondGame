using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelUIButtonHelper : MonoBehaviour
{
    public NodeInfo nodeInfo;
    public LevelDesigner LD;
    public ProcGenInputHandling PCGinput;
    public TextMeshProUGUI textBox;
    private Image image => GetComponent<Image>();

    public List<string> layerNames = new List<string>();
    public TextMeshProUGUI layerNameTextBox;



    public void setupButton(NodeInfo newInfo) 
    {
        nodeInfo = newInfo;
        image.color = newInfo.material.color;
        textBox.text = nodeInfo.name;
        PCGinput = Camera.main.GetComponent<ProcGenInputHandling>();
    }

    public void clicked()
    {
        PCGinput.currInfo = nodeInfo;

    }


    public void LayerUp(){
   
        if(LD.Layers[LD.currentLayerType].currentLayer
                < LD.Layers[LD.currentLayerType].layerParents.Count - 1) 
        {
            LD.Layers[LD.currentLayerType].layerParents[LD.Layers[LD.currentLayerType].currentLayer].SetActive(false);
            LD.Layers[LD.currentLayerType].currentLayer++;
            LD.Layers[LD.currentLayerType].layerParents[LD.Layers[LD.currentLayerType].currentLayer].SetActive(true);
            textBox.text = "Current Layer : " + (LD.Layers[LD.currentLayerType].currentLayer + 1);
        }
    }

    public void LayerDown(){

        if(LD.Layers[LD.currentLayerType].currentLayer > 0) 
        {
            LD.Layers[LD.currentLayerType].layerParents[LD.Layers[LD.currentLayerType].currentLayer].SetActive(false);
            LD.Layers[LD.currentLayerType].currentLayer--;
            LD.Layers[LD.currentLayerType].layerParents[LD.Layers[LD.currentLayerType].currentLayer].SetActive(true);
            textBox.text = "Current Layer : " + (LD.Layers[LD.currentLayerType].currentLayer + 1);
        }
    }
    
    public void NewLayer(){
        if(LD.currentLayerType >= 1) 
        {
            LD.Layers[LD.currentLayerType].layerParents
                    .Add(LD.createLayer(LD.currentLayerType, new Vector3(54, 0, 21)));
        }
    }


    public void SwitchToLayer(int _layer) {
        textBox.text = "Current Layer : 1";
        LD.Layers[LD.currentLayerType].layerParents[LD.Layers[LD.currentLayerType].currentLayer].SetActive(false);      
        LD.Layers[_layer].layerParents[0].SetActive(true);
        LD.currentLayerType = _layer;
        LD.Layers[LD.currentLayerType].currentLayer = 0;
        layerNameTextBox.text = layerNames[_layer - 1];
    }



}
