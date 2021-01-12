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
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        return NodeState.SUCCESS;
    }
}
