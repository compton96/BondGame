// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackSunBeam : BTLeaf
{
    creatureAttackRanged attack;
    public CActionAttackSunBeam(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play anim
        context.animator.Attack1();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnSunBeam(attack.projectile, context.targetEnemy, attack.baseDmg, attack.abilityBuff);
        
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
