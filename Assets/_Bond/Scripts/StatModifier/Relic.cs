using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "ScriptableObjects/Relic")]
public class Relic : ScriptableObject
{
    public string relicName;
    public string relicInfo;
    //sprite
    [SerializeField]
    public List<Modifier> modifiers = new List<Modifier>();
}
