using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class StatManager : MonoBehaviour
{
    public BaseStats baseStats;
    public Dictionary<ModiferType, Stat> stats = new Dictionary<ModiferType, Stat>();
    protected Dictionary<Buff, float> buffs = new Dictionary<Buff, float>();

    public float getStat(ModiferType _modiferType){
        return stats[_modiferType].modifiedValue;
    }

    public void setStat(ModiferType _modiferType, float value)
    {
        if(stats.ContainsKey(_modiferType))
        {
            stats[_modiferType].modifiedValue = value;
        }
    }

    private void Awake() 
    {
        initStats();
    }

    private void initStats() {
        foreach(baseStat stat in baseStats.stats)
        {
            stats.Add(stat.modiferType, new Stat(stat.modiferType, stat.baseValue));
            stats[stat.modiferType].UpdateValue();
        }
    }

    public void AddRelic(List<Modifier> mods)
    {
        foreach(Modifier mod in mods) 
        {
            AddModifier(mod);
        }
    }

    private void AddModifier(Modifier _modifier)
    {
        if(stats.ContainsKey(_modifier.modiferType))
        {
            Debug.Log("adding modifier " + _modifier.modiferType + " value " + _modifier.Additive);
            stats[_modifier.modiferType].AddModifier(_modifier);
        }
    }

    private void RemoveModifier(Modifier _modifier)
    {
        if(stats.ContainsKey(_modifier.modiferType))
        {
            stats[_modifier.modiferType].RemoveModifier(_modifier);
        }
    }
    
    public void AddBuff(Buff _buff)
    {
        //Check if buff already added
        foreach(KeyValuePair<Buff, float> buff in buffs)
        {
            if(_buff.buffName == buff.Key.buffName)
            {
                return;
            }
        }

        //Add buffs
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].AddModifier(mod);
                // Debug.Log("Added buff");
            }
        }
        buffs.Add(_buff, _buff.buffDuration);
    }

    public void RemoveBuff(Buff _buff){
        
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].RemoveModifier(mod);
            }
        }
        buffs.Remove(_buff);
    }

    public void TakeDamage(float baseAmount, ModiferType damageType) 
    {
        stats[ModiferType.CURR_HEALTH].modifiedValue -= (baseAmount * (1 - stats[damageType].modifiedValue)); // FORMULA FOR DAMAGE RESISTANCE;
        // Debug.Log("Took " + (baseAmount * (1 - stats[damageType].modifiedValue)) + " damage");
        // Debug.Log("Base amount was " + baseAmount);

    }

    public void TakeDamageCreature(float baseAmount, ModiferType damageType) 
    {
        stats[ModiferType.CURR_ENTHUSIASM].modifiedValue -= (baseAmount);
    }


    private void Update() {
        ProcessBuffs();
    }

    private void ProcessBuffs()
    {
        for(int i = 0; i < buffs.Count; i++){ 
            var key = buffs.ElementAt(i).Key;
            var value = buffs.ElementAt(i).Value;
            buffs[key] = value - Time.deltaTime;
            if(buffs[key] <= 0)
            {
                RemoveBuff(key);
            }
        }
    }
    
}
