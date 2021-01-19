using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionBarbaricMeleeAttack : BTLeaf
{
    creatureAttackMelee attack;
    public CActionBarbaricMeleeAttack(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackMelee) context.CD.abilities[context.lastTriggeredAbility];
        //Play amim
        context.animator.Attack1();
   
    }

    protected override void OnExit()
    {
    }

    public override NodeState Evaluate() 
    {
        // context.targetEnemy.GetComponent<EnemyStats>().takeDamage(attack.baseDmg);
        if(Random.Range(0f,1f) < 0.5)
        {
            // context.targetEnemy.GetComponent<EnemyStats>().takeDamage(attack.baseDmg);
        } 
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            return NodeState.SUCCESS;
        }
        

    }
}
