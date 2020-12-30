// Jake, Colin, Eugene, Riko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTNode
{
    protected BTNode node;
    
    public BTInverter(string _name, BTNode node) : base(_name) 
    {
        name = _name;
        this.node = node;
    }
    
    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                return _nodeState;
            default:
                break;
        }
        return _nodeState;
    }
}
