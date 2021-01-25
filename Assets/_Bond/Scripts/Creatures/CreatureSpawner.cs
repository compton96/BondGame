using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public List<Personality> PowerPersonalities = new List<Personality>();
    public List<Personality> UtilityPersonalities = new List<Personality>();
    public List<Personality> DexterityPersonalities = new List<Personality>();

    public List<creatureData> creatureTypes = new List<creatureData>();


    [ContextMenuItem("SpawnCreature", "SpawnCreature")]
    [SerializeField]
    private float test;
    
    public Transform spawnPoint;

    private void Awake()
    {
        SpawnCreature();
    }

    public void SpawnCreature()
    {
        //pick random creature and spawn it in the world.
        int _randomCreatureNumber = Random.Range(0, creatureTypes.Count);
        GameObject Creature = Instantiate(creatureTypes[_randomCreatureNumber].creaturePrefab, spawnPoint.position, Quaternion.identity);
        
        //get access to its active data, and assign its values.
        ActiveCreatureData _ActiveCreatureData =  Creature.GetComponent<ActiveCreatureData>();
        _ActiveCreatureData.Id = Creature.GetInstanceID();
        _ActiveCreatureData.statManager.stats[ModiferType.MAX_HEALTH].baseValue = 
                Random.Range(creatureTypes[_randomCreatureNumber].lifeRange.x, creatureTypes[_randomCreatureNumber].lifeRange.y);
        _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_POWER].baseValue =  
                (int) Random.Range(creatureTypes[_randomCreatureNumber].powerRange.x, creatureTypes[_randomCreatureNumber].powerRange.y);
        _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_UTILITY].baseValue = 
                (int) Random.Range(creatureTypes[_randomCreatureNumber].utilityRange.x, creatureTypes[_randomCreatureNumber].utilityRange.y);
        _ActiveCreatureData.statManager.stats[ModiferType.CREATURE_DEXTERITY].baseValue = 
                (int) Random.Range(creatureTypes[_randomCreatureNumber].dexterityRange.x, creatureTypes[_randomCreatureNumber].dexterityRange.y);
        
        //select random abilities;
        List<creatureAttackBase> _copyOfAttacks = new List<creatureAttackBase>(creatureTypes[_randomCreatureNumber].creatureAttacks); //make a copy of the list of abilities so we can edit it without losing data
        
        int _firstAttackNumber = Random.Range(0, _copyOfAttacks.Count);
        _ActiveCreatureData.abilities.Add(_copyOfAttacks[_firstAttackNumber]);
        _copyOfAttacks.RemoveAt(_firstAttackNumber);

        int _secondAttackNumber = Random.Range(0, _copyOfAttacks.Count);
        _ActiveCreatureData.abilities.Add(_copyOfAttacks[_secondAttackNumber]);
        _copyOfAttacks.RemoveAt(_secondAttackNumber);

        _ActiveCreatureData.abilities[0].id = 1;
        _ActiveCreatureData.abilities[1].id = 2;


        //select personalities
        _ActiveCreatureData.personalities = choosePersonalities(_ActiveCreatureData, creatureTypes[_randomCreatureNumber]);
        if(_ActiveCreatureData.personalities.Count > 0) 
        {
            Creature.name = _ActiveCreatureData.personalities[0].name + "Fragaria";
        } else
        {
            Creature.name = "Default Fragaria";
        }
        
        
        //build BT
        Creature.GetComponent<CreatureAIContext>().GetActiveCreatureData();
        Creature.GetComponent<CreatureAI>().BuildBT();
        

    }

    List<Personality> choosePersonalities(ActiveCreatureData _ActiveCreatureData, creatureData _CreatureData){
        List<Personality> finalPersonalities = new List<Personality>();
        foreach (Personality Personality in PowerPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_POWER].baseValue,
                                      _CreatureData.powerRange.x,
                                      _CreatureData.powerRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        foreach (Personality Personality in UtilityPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_UTILITY].baseValue,
                                      _CreatureData.utilityRange.x,
                                      _CreatureData.utilityRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        foreach (Personality Personality in DexterityPersonalities) 
        {
            if(Personality.calcChance(_ActiveCreatureData.statManager.stats[ModiferType.CREATURE_DEXTERITY].baseValue,
                                      _CreatureData.dexterityRange.x,
                                      _CreatureData.dexterityRange.y)) 
            {
                finalPersonalities.Add(Personality);
                break;
            }
        }
        return finalPersonalities;
    }

}


// public enum statModifierType 
// {
//     POWER, UTILITY, DEXTERITY, LIFE, BONDRATE, NONE
// }