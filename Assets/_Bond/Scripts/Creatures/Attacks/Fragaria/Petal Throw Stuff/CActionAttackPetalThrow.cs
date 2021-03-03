
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionAttackPetalThrow : BTLeaf
{
    creatureAttackRanged attack;
    private NavMeshAgent agent;
    public CActionAttackPetalThrow(string _name, CreatureAIContext _context ) : base(_name, _context)
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
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnPetals(context.PetalCone);
        if (context.enemyList != null)
        {
            foreach (GameObject enemy in context.enemyList)
            {
                context.abilitySpawner.GetComponent<AbilitySpawner>()
                    .SpawnProjectile(attack.projectile, enemy, attack.projectileSpeed, attack.baseDmg, true);
            }
        }

        context.enemyList.Clear();
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
        

    }
}
