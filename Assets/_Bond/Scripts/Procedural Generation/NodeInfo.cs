using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeInfo", menuName = "ScriptableObjects/NodeInfo", order = 1)]
public class NodeInfo : ScriptableObject
{
    public string name;
    public Material material;
    public float height;
    public List<GameObject> nodes = new List<GameObject>();
}
