// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackTurtleTop : BTLeaf
{
    creatureAttackMelee attack;
    public CActionAttackTurtleTop(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play anim
        context.animator.TurtleTop();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
        

    }
}
