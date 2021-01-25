using UnityEngine;
using System.Collections;
using System;

public class BTUntilSucceed : BTNode
{
    protected BTNode node;
    
    public BTUntilSucceed(string _name, BTNode node) : base(_name) 
    {
        name = _name;
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        if ((node.OnParentEvaluate()) == NodeState.SUCCESS) // If given a success return
        {
            return NodeState.SUCCESS;
        } else
        {
            return NodeState.RUNNING;
        }
    }

}