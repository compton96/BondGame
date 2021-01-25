using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Modifier
{
    public string modifierIdentifier;
    public ModiferType modiferType;
    public float Additive;
    public float Multiplicitive;
}

public enum ModiferType
{
    MAX_HEALTH, 
    CURR_HEALTH, 
    DAMAGE, 
    MOVESPEED, 
    MELEE_RESISTANCE, 
    RANGED_RESISTANCE,
    GOLD_GAIN,
    SHOP_DISCOUNT,
    ATTACK_CHARGE_TIME,
    HEAVY_ATTACK_AOE,
    ATTACK_SPEED,
    COMBO_AMOUNT, // idk if well actually implement this or not 
    CRIT_CHANCE,
    CRIT_DAMAGE,
    ATTACK_RANGE,
    DASH_SPEED,
    DASH_RANGE,
    DASH_COOLDOWN,
    DETECTION_RANGE,
    BUFF_EFFECTIVENESS,
    CREATURE_DEXTERITY,
    CREATURE_UTILITY,
    CREATURE_POWER
}