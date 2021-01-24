using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Personality", menuName = "ScriptableObjects/Personality", order = 1)]
public class Personality : ScriptableObject
{


    public string personalityName; //personality name
    
    public float basePercentage; //base chance that a creature can roll this personality
    public float maxPercentage;

    public ModiferType statModType1; //the stat that this personality will modify
    public float statModifierAmount1; // amount that this personality affects the stat.
    
    public ModiferType statModType2; //optional secondary 
    public float statModifierAmount2;

    public BTSubtree WildNoticed;
    public BTSubtree WildAlone;
    public BTSubtree AbilityTree;
    public BTSubtree FollowPlayerTree;


    public bool calcChance(float roll, float minRoll, float MaxRoll)
    {
        float chance = linearMap(roll, minRoll, MaxRoll, basePercentage, maxPercentage);
        if (chance >= Random.Range(0f,1f))
        {
            return true;
        } else
         {
            return false;
        }
    }

    public float linearMap(float value, float inputLow, float inputHigh, float outputLow, float outputHigh) 
    {
        return outputLow + (outputHigh - outputLow) * (value - inputLow) / (inputHigh - inputLow);
    }

}
