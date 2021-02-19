using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "ScriptableObjects/RelicStats")]
public class RelicStats : ScriptableObject
{
    public string relicName;
    public string relicInfo;
    //sprite
    [SerializeField]
    public List<Modifier> modifiers = new List<Modifier>();
}
