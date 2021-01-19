// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionHeartyWildApproachDroppedFood : BTLeaf
{
    private NavMeshAgent agent;
    private float moveSpeed = 5f;
    private float angularSpeed = 720f; //deg/s
    private float acceleration = 100f; //max accel units/sec^2

    public CActionHeartyWildApproachDroppedFood(string _name, CreatureAIContext _context) : base(_name, _context) 
    {
        name = _name;
        context = _context;

        agent = context.agent;
        //ObjToFollow = captCreature.followPoint;
        agent.autoBraking = true;
        agent.autoRepath = false;
        agent.angularSpeed = angularSpeed;
        agent.acceleration = acceleration;
        //agent.speed = moveSpeed;
    }

    protected override void OnEnter()
    {
        agent.speed = context.CD.moveSpeed;
    }

    protected override void OnExit()
    {
        context.doMovement(0f);
        agent.ResetPath();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Approaching dropped food");
        agent.destination = context.foundFood.transform.position;
        if(Vector3.Distance(context.foundFood.transform.position, context.creatureTransform.position) < 3) 
        {
            // Made it to fruit
            Fruit fruitScript = context.foundFood.GetComponent<Fruit>();
            //Eat the fruit
            fruitScript.Destroy();
            // Creature is now befriended
            context.isWild = false;
            PlayerController playerController = context.player.GetComponent<PlayerController>();
            playerController.currCreature = context.creatureTransform.gameObject;
            playerController.currCreatureContext = context;

            OnParentExit();
            return NodeState.SUCCESS;
        } else 
        {
            // Keep going to fruit
            return NodeState.RUNNING;
        }
    }
}
