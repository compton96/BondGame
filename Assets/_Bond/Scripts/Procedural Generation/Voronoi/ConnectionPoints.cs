using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConnectionPoints", menuName = "ScriptableObjects/ConnectionPoints")]
public class ConnectionPoints : ScriptableObject
{
    public List<Vector2Pair> points = new List<Vector2Pair>();
}

[System.Serializable]
public class Vector2Pair
{
	public Vector2 start;
	public Vector2 end;
	public Biome biome;
}