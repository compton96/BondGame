// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTriggers : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SlashHitSFX;

    [FMODUnity.EventRef]
    public string CritSFX;

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
                // Debug.Log("crit hit");
            }

            // Check which attack state we're in to determine damage
            if(fsm.currentState == fsm.Slash0)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1.2f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
            } 
            else if(fsm.currentState == fsm.Slash1)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
            } 
            else if(fsm.currentState == fsm.Slash2)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
            } 
            else if(fsm.currentState == fsm.Slash3)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
            } 
            else if(fsm.currentState == fsm.Slash4)
            {
                enemyAIContext.statManager.TakeDamage( damage * 1.5f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
            } 
            else if(fsm.currentState == fsm.HeavySlash)
            {
                enemyAIContext.statManager.TakeDamage( damage * 2f, ModiferType.MELEE_RESISTANCE);
                FMODUnity.RuntimeManager.PlayOneShot(SlashHitSFX, transform.position);

                checkForCrit(damage);
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
    
    private void checkForCrit( float damage )
    {
        if (damage != ps.getStat(ModiferType.DAMAGE))
        {
            FMODUnity.RuntimeManager.PlayOneShot(CritSFX, transform.position);
        }
    }
}
