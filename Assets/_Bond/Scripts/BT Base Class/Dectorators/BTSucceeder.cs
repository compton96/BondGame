using UnityEngine;
using System.Collections;
using System;

public class BTSucceeder: BTNode
{
    protected BTNode node;

    public BTSucceeder(string _name, BTNode node) : base(_name) 
    {
        name = _name;
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        if ((node.Evaluate()) == NodeState.RUNNING) // If given a success return
        {
            return NodeState.RUNNING;
        } else
        {
            return NodeState.SUCCESS;
        }
    }

}