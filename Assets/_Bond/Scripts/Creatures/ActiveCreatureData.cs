using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCreatureData : MonoBehaviour
{
    public bool ready = false;

    public StatManager statManager => gameObject.GetComponent<StatManager>();

    public int Id;


    public List<Personality> personalities = new List<Personality>();
    
    public List<creatureAttackBase> abilities = new List<creatureAttackBase>();
}
