using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float baseValue;
    public float modifiedValue;
    //maybe add hard caps?
    public ModiferType modiferType;
    public List<Modifier> modifiers = new List<Modifier>();

    public Stat(ModiferType _modiferType, float _baseValue)
    {
        modiferType = _modiferType;
        baseValue = _baseValue;
        modifiedValue = _baseValue;
    }

    public void AddModifier(Modifier _modifer) 
    {
        modifiers.Add(_modifer);
        UpdateValue();
    }

    public void RemoveModifier(Modifier _modifier) 
    {
        modifiers.Remove(_modifier); //TEST THIS WE DONT KNOW IF IT WILL KNOW WHICH TO REMOVE
        UpdateValue();
    }

    public void UpdateValue()
    {
        modifiedValue = baseValue; //update this to incorporate modifiers.
    }


    //list of buffs/debuffs that are constantly ticking, and when are at 0, are removed from the list?
}
