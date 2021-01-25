using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCreatureData : MonoBehaviour
{
    public bool ready = false;

    public float maxLife;
    public int level; //??

    public int power;
    public int utility;
    public int dexterity;
    
    public float moveSpeed;
    
    public int Id;



    public List<Personality> personalities = new List<Personality>();
    
    public List<creatureAttackBase> abilities = new List<creatureAttackBase>();
}
