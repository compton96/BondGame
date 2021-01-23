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
            EnemyAIContext enemyAIContext = other.gameObject.GetComponent<EnemyAIContext>();

            // Check which attack state we're in to determine damage
            if(fsm.currentState == fsm.Slash0)
            {
                enemyAIContext.takeDamage(ps.attack1Damage);
            } 
            else if(fsm.currentState == fsm.Slash1)
            {
                enemyAIContext.takeDamage(ps.attack2Damage);
            } 
            else if(fsm.currentState == fsm.Slash2)
            {
                enemyAIContext.takeDamage(ps.attack3Damage);
            } 
            else if(fsm.currentState == fsm.HeavySlash)
            {
                enemyAIContext.takeDamage(ps.heavyDamage);
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
