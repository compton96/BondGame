﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTriggers : MonoBehaviour
{
    private PlayerStats ps;
    private PlayerStateMachine fsm;
    // Start is called before the first frame update
    private void Start() 
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        fsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyAIContext enemyAIContext = other.gameObject.GetComponent<EnemyAIContext>();

            // Check which attack state we're in to determine damage
            if(fsm.currentState == fsm.Slash0)
            {
                enemyAIContext.statManager.TakeDamage(ps.attack1Damage, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);
            } 
            else if(fsm.currentState == fsm.Slash1)
            {
                enemyAIContext.statManager.TakeDamage(ps.attack2Damage, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);
            } 
            else if(fsm.currentState == fsm.Slash2)
            {
                enemyAIContext.statManager.TakeDamage(ps.attack3Damage, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);
            } 
            else if(fsm.currentState == fsm.HeavySlash)
            {
                enemyAIContext.statManager.TakeDamage(ps.heavyDamage, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/SFX/Sword Impact Enemy", transform.position);
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
