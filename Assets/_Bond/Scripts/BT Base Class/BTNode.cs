// Jake, Colin, Eugene, Riko
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class BTNode //not a monobehavior, should never be on a object.
{
    protected string name; //storing name for debuging purposes

    protected BTNode(string _name){  //constructor that takes in name, protected so you can overwrite, and also call back to.
        name = _name;
    }

    protected NodeState _nodeState; //current node state.

    public NodeState nodeState { get{return _nodeState;} } //get current node state.


    public abstract NodeState Evaluate(); //Do the main function of the node, return a NodeState

    public virtual NodeState OnParentEvaluate()
    {
        return Evaluate();
    }
}

public enum NodeState
{
    RUNNING, SUCCESS, FAILURE, // Actual states of task
}
