// Jake
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
            EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();

            // Check which attack state we're in to determine damage
            if(fsm.currentState == fsm.Slash0)
            {
                enemyStats.takeDamage(ps.attack1Damage);
            } 
            else if(fsm.currentState == fsm.Slash1)
            {
                enemyStats.takeDamage(ps.attack2Damage);
            } 
            else if(fsm.currentState == fsm.Slash2)
            {
                enemyStats.takeDamage(ps.attack3Damage);
            } 
            else if(fsm.currentState == fsm.HeavySlash)
            {
                enemyStats.takeDamage(ps.attack3Damage);
            } 
            else
            {
                Debug.Log("Default Damage");
                other.gameObject.GetComponent<EnemyStats>().takeDamage(ps.attack1Damage);
            }
            
        } 
        else if(other.gameObject.tag == "FruitTree")
        {
            print("Hit tree with sword");
            other.gameObject.GetComponent<FruitTree>().dropFruit();
        }
    }
}
