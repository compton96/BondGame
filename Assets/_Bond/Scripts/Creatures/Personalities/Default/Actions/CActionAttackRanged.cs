using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackRanged : BTLeaf
{
    creatureAttackRanged attack;
    public CActionAttackRanged(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
        Debug.Log("Attacking");
        context.animator.Attack1();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() {
        //Debug.Log("ATTACK RANGED");
        context.projectileSpawner.GetComponent<ProjectileSpawner>()
            .SpawnProjectile(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDmg, attack.isHoming);
        
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            context.cooldownSystem.PutOnCooldown(context.creatureStats.abilities[context.lastTriggeredAbility]);
            return NodeState.SUCCESS;
        }
        

    }
}
