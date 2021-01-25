using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StatManager : MonoBehaviour
{
    public BaseStats baseStats;
    public Dictionary<ModiferType, Stat> stats = new Dictionary<ModiferType, Stat>();
    protected Dictionary<Buff, float> buffs = new Dictionary<Buff, float>();

    public float getStat(ModiferType _modiferType){
        return stats[_modiferType].modifiedValue;
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


    private void AddModifier(Modifier _modifier)
    {
        if(stats.ContainsKey(_modifier.modiferType))
        {
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
    
    public void AddBuff(Buff _buff){
        
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].AddModifier(mod);
            }
        }
        buffs.Add(_buff, _buff.buffDuration);
    }

    private void RemoveBuff(Buff _buff){
        
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].RemoveModifier(mod);
            }
        }
    }

    public void TakeDamage(float baseAmount, ModiferType damageType) 
    {
        stats[ModiferType.CURR_HEALTH].modifiedValue -= (baseAmount * (1 - stats[damageType].modifiedValue)); // FORMULA FOR DAMAGE RESISTANCE;
        // Debug.Log("Took " + (baseAmount * (1 - stats[damageType].modifiedValue)) + " damage");
        // Debug.Log("Base amount was " + baseAmount);

    }


    private void Update() {
        ProcessBuffs();
    }

    private void ProcessBuffs()
    {
        foreach(KeyValuePair<Buff, float> buff in buffs){ 
            buffs[buff.Key] = buff.Value - Time.deltaTime;
            if(buffs[buff.Key] <= 0)
            {
                RemoveBuff(buff.Key);
                buffs.Remove(buff.Key);
            }
        }
    }
    
}
