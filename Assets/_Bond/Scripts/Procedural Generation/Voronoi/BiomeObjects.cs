using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeObjects", menuName = "ScriptableObjects/BiomeObjects")]
public class BiomeObjects : ScriptableObject {
    List<GameObject> smallObjects = new List<GameObject>();
    List<GameObject> mediumObjects = new List<GameObject>();
    List<GameObject> largeObjects = new List<GameObject>();
}