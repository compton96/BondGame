﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTriggers : MonoBehaviour
{
    private StatManager ps;
    private PlayerStateMachine fsm;
    // Start is called before the first frame update
    private void Start() 
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<StatManager>();
        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyAIContext enemyAIContext = other.gameObject.GetComponent<EnemyAIContext>();

            float damage = ps.getStat(ModiferType.DAMAGE);

            //Check for crits
            int range = (int) Mathf.Round(100/ps.getStat(ModiferType.CRIT_CHANCE));
            if(Random.Range(1, range) == 1)
            {
                damage *= ps.getStat(ModiferType.CRIT_DAMAGE);
                Debug.Log("crit hit");
            }

            // Check which attack state we're in to determine damage
            if(fsm.currentState == fsm.Slash0)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1.1f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);

                if (damage != ps.getStat(ModiferType.DAMAGE))
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Crit", transform.position);
                }
            } 
            else if(fsm.currentState == fsm.Slash1)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1.2f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);

                if (damage != ps.getStat(ModiferType.DAMAGE))
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Crit", transform.position);
                }
            } 
            else if(fsm.currentState == fsm.Slash2)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1.25f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);

                if (damage != ps.getStat(ModiferType.DAMAGE))
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Crit", transform.position);
                }
            } 
            else if(fsm.currentState == fsm.HeavySlash)
            {
                enemyAIContext.statManager.TakeDamage( damage * 2f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);

                if (damage != ps.getStat(ModiferType.DAMAGE))
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Crit", transform.position);
                }
            } 
            // else
            // {
            //     Debug.Log("Default Damage");
            //     other.gameObject.GetComponent<EnemyAIContext>().takeDamage(ps.attack1Damage);
            // }
            
        } 
        else if(other.gameObject.tag == "FruitTree")
        {
            print("Hit tree with sword");
            other.gameObject.GetComponent<FruitTree>().dropFruit();
        }
    }
}
