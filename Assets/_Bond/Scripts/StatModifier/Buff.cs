using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "ScriptableObjects/Buff")]
public class Buff : ScriptableObject
{
    public string buffName;
    //store a sprite
    public float buffDuration;

    public List<Modifier> modifiers = new List<Modifier>();
}
