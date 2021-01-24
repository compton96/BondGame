using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "ScriptableObjects/BaseStats")]
public class BaseStats : ScriptableObject
{
    public string statName;
    [SerializeField]
    public List<baseStat> stats = new List<baseStat>();
}



[System.Serializable]
public class baseStat
{
    public ModiferType modiferType;
    public float baseValue;
}