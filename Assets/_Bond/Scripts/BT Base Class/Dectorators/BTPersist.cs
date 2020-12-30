using UnityEngine;
using System.Collections;
using System;

public class BTPersist: BTNode
{
    protected BTNode node;

    public BTPersist(string _name, BTNode node) : base(_name) 
    {
        name = _name;
        this.node = node;
    }
    
    public override NodeState Evaluate()
    {
        return NodeState.RUNNING;
    }

}