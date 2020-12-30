using UnityEngine;
using System.Collections;
using System;

public class BTUntilFailure: BTNode
{
    protected BTNode node;
    
    public BTUntilFailure(string _name, BTNode node) : base(_name) 
    {
        name = _name;
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        if ((node.Evaluate()) == NodeState.FAILURE) // If given a success return
        {
            return NodeState.FAILURE;
        } else 
        {
            return NodeState.RUNNING;
        }
    }

}