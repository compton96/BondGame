using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionPlayHitstunAnim : BTLeaf
{
    public EActionPlayHitstunAnim(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    protected override void OnEnter()
    {
        //Play hitstun anim
        enemyContext.animator.Hitstun();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        // return NodeState.SUCCESS;
        if(enemyContext.animator.inHitstun)
        {
            return NodeState.RUNNING;
        } else 
        {
            enemyContext.tookDamage = false;
            OnParentExit();
            return NodeState.SUCCESS;
        }
    }
}
