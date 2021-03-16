// Jake and Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeObjects", menuName = "ScriptableObjects/BiomeObjects")]
public class BiomeObjects : ScriptableObject 
{
    public List<BiomeSpecificAssetList> Assets = new List<BiomeSpecificAssetList>();
}

[System.Serializable]
public class BiomeSpecificAssetList 
{
    public float percentage;
    public List<GameObject> objects = new List<GameObject>();
}