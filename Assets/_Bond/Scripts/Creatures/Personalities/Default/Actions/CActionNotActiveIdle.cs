using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionNotActiveIdle : BTLeaf
{
    float enthusiasmGainMultiplier = 1;

    public CActionNotActiveIdle(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate()
    {   
        if(context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) < context.creatureStats.statManager.getStat(ModiferType.MAX_ENTHUSIASM)){
            context.creatureStats.statManager.setStat(ModiferType.CURR_ENTHUSIASM, context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) + Time.deltaTime * enthusiasmGainMultiplier);
        }
        
        return NodeState.SUCCESS;
    }
}
