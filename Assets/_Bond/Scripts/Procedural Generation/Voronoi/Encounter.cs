using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Encounter", menuName = "ScriptableObjects/Encounter")]
public class Encounter : ScriptableObject 
{
    public GameObject encounter;
    public List<GameObject> indicators = new List<GameObject>();
}
