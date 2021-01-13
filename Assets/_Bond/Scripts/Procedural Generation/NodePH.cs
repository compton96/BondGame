using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePH : MonoBehaviour
{
    public int index;
    public Vector2 pos;
    public NodeInfo nodeInfo;
    public LevelDesigner LevelDesigner;
    private Renderer renderer => GetComponent<Renderer>();
    public int myLayer;

    public void onClicked(NodeInfo newInfo) {
        nodeInfo = newInfo;
        renderer.material = newInfo.material;
        switch (nodeInfo.name)
        {
            case "N" :
                LevelDesigner.IGen.connectors.northConnector = pos;
                break;
            case "E" :
                LevelDesigner.IGen.connectors.eastConnector = pos;
                break;
            case "S" :
                LevelDesigner.IGen.connectors.southConnector = pos;
                break;
            case "W" :
                LevelDesigner.IGen.connectors.westConnector = pos;
                break;
            default:
                break;
        }

        LevelDesigner.updateNode(index, newInfo, myLayer);
    }
}
