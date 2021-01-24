using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public BaseStats baseStats;
    public Dictionary<ModiferType, Stat> stats = new Dictionary<ModiferType, Stat>();
    protected Dictionary<Buff, float> buffs = new Dictionary<Buff, float>();

    private void Awake() 
    {
        initStats();
    }

    private void initStats() {
        foreach(baseStat stat in baseStats.stats)
        {
            stats.Add(stat.modiferType, new Stat(stat.modiferType, stat.baseValue));
        }
    }


    private void addModifer(Modifier _modifier)
    {
        if(stats.ContainsKey(_modifier.modiferType))
        {
            stats[_modifier.modiferType].addModifer(_modifier);
        }
    }

    private void removeModifier(Modifier _modifier)
    {
        if(stats.ContainsKey(_modifier.modiferType))
        {
            stats[_modifier.modiferType].removeModifier(_modifier);
        }
    }
    
    public void addBuff(Buff _buff){
        
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].addModifer(mod);
            }
        }
        buffs.Add(_buff, _buff.buffDuration);
    }

    private void removeBuff(Buff _buff){
        
        foreach(Modifier mod in _buff.modifiers)
        {
            if(stats.ContainsKey(mod.modiferType))
            {
                stats[mod.modiferType].removeModifier(mod);
            }
        }
    }

    public void takeDamage(float baseAmount, ModiferType damageType) 
    {
        stats[ModiferType.CURR_HEALTH].modifiedValue -= (baseAmount * (1 - stats[damageType].modifiedValue)); // FORMULA FOR DAMAGE RESISTANCE;
        // Debug.Log("Took " + (baseAmount * (1 - stats[damageType].modifiedValue)) + " damage");
        // Debug.Log("Base amount was " + baseAmount);
    }


    private void Update() {
        processBuffs();
    }

    private void processBuffs()
    {
        foreach(KeyValuePair<Buff, float> buff in buffs){ 
            buffs[buff.Key] = buff.Value - Time.deltaTime;
            if(buffs[buff.Key] <= 0)
            {
                removeBuff(buff.Key);
                buffs.Remove(buff.Key);
            }
        }
    }
    
}
