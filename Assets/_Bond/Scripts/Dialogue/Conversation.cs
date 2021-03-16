using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject {
    
    [Header("Dialog Trigger Set-up")]
    public DialogTrigger trigger;
    public float triggerAmount;
    [Header("Dialog : ")]
    public List<string> dialog = new List<string>();

    [Header("programmer stuff no touchy")]
    public int step;
}


public enum DialogTrigger
{
    SWORD_SWINGS,
    WINS,
    LOSSES,
    TIMES_TALKED_TO,
    CREATURES_BEFRIENDED,
    CREATURES_SAVED,
    RELICS_DISCOVERED,
}   










