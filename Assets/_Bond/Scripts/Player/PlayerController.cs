//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public struct Inputs
    {
        public bool dash; //Dash and interact are used for same button press, possibly refactor?
        public bool interact;
        public bool basicAttack;
        public bool heavyAttack;
        public Vector3 moveDirection;
        public Vector2 rawDirection;
    }

    public Inputs inputs;

    public Camera camera;
    public bool isoMovement = true;

    public GameObject fruit;
    public PlayerStateMachine fsm => GetComponent<PlayerStateMachine>();
    public PlayerAnimator animator => GetComponent<PlayerAnimator>();
    public PlayerStats stats => GetComponent<PlayerStats>();

    //*******Dash Variables*******
    public Vector3 facingDirection;
    public float dashSpeed;
    public float dashTime;
    public float dashDelay = 1.2f;
    public float dashStart = 2;
    public int dashCount = 0;
    public bool isDashing = false;
    //****************************

    private Rigidbody rb;
    
    private CharacterController charController;
    
    private Vector3 gravity;

    public float crouchModifier = 1;
    public bool nearInteractable = false;
    

    public GameObject wildCreature = null;
    public GameObject currCreature;
    public GameObject interactableObject;
    public CreatureAIContext currCreatureContext;
    public float currSpeed;
    public CapsuleCollider swordCollider; 
    public bool isHit;

    public ParticleSystem heavyChargeVfx;
    public ParticleSystem heavyHitVfx;

    public float isoSpeedADJ = 0f;


    [Serializable]
    public struct HitBoxes
    {
        public GameObject slash0;
        public GameObject slash1;
        public GameObject slash2;
        public GameObject slash3;
        public GameObject heavy;

    }

    public HitBoxes hitBoxes;

    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        dashStart = Time.time;
        animator.ResetAllAttackAnims();
        
       
    }

   

    public void doMovement(float movementModifier)
    {
        if(!charController.isGrounded)
        {
            gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravity = Vector3.zero;
        }

        // HERMAN TODO: Break up massive math formula into different variables
        currSpeed = (Mathf.Abs(inputs.moveDirection.x) + Mathf.Abs(inputs.moveDirection.z)) / 2 * stats.speed * Time.deltaTime * movementModifier * crouchModifier;
        var movementVector = inputs.moveDirection * stats.speed * Time.deltaTime * movementModifier * crouchModifier;
        charController.Move(movementVector);
        charController.Move(gravity * Time.deltaTime);

        animator.Move(movementVector);
    }

    public void doRotation(float rotationModifier)
    {
        if(inputs.rawDirection != Vector2.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputs.moveDirection, Time.deltaTime * stats.turnSpeed * rotationModifier);
        }
    }

    public void setRotation(Vector3 vec)
    {
        transform.forward = vec;
    }

    //********* INPUT FUNCTIONS **********
    private void OnMovement(InputValue value)
    {
        //Debug.Log(value.Get<Vector2>());
        inputs.rawDirection = value.Get<Vector2>();
        inputs.rawDirection.Normalize();
        inputs.rawDirection.y *= isoSpeedADJ;

        inputs.moveDirection = new Vector3(inputs.rawDirection.x, 0, inputs.rawDirection.y);


        if(isoMovement)
        {
            inputs.moveDirection = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * inputs.moveDirection;
        }

        if(inputs.moveDirection != Vector3.zero) facingDirection = inputs.moveDirection;
        
    }

    

    //by Jamo
    private void OnInteract() //pressing dash button  
    {     
        //If near something interactable, this overides the dash
        if(nearInteractable)
        {
            inputs.interact = true;
            if(interactableObject != null)
            {
                //Debug.Log("picked up item");
                Destroy(interactableObject);
                nearInteractable = false;
            }
            else if (wildCreature != null)
            {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                currCreature = wildCreature;
                currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
                //FIX LATER --- NEED TO DISABLE NOTICE/INTERACT COLLIDERS
                nearInteractable = false;
            }
        }
        else//Not near interactable, dash instead
        {
            if(Time.time > dashStart + dashDelay)//cant dash until more time than dash delay has elapsed,
            {
                //takes dash start time
                dashStart = Time.time;
                dashCount++;
                inputs.dash = true;
            }
            else if(dashCount >= 1 )//if you have dashed once and are not past delay, you can dash a second time
            {
                dashCount = 0;
                inputs.dash = true;          
                
            }   
        }                 
    }

   
    //Slash (X)
    private void OnAttack1()
    {
        inputs.basicAttack = true;
    }


    //Holding X
    private void OnHeavyAttack(InputValue value)
    {
        float val = value.Get<float>();

        if(val == 1) inputs.heavyAttack = true;
        else inputs.heavyAttack = false;
        
    }


    //creature ability 1 (Y)
    private void OnAttack2()
    {
        currCreatureContext.isAbilityTriggered = true;
        currCreatureContext.lastTriggeredAbility = 0;
    }  


    //creature ability 2 (B)
    private void OnAttack3()
    {
        currCreatureContext.isAbilityTriggered = true;
        currCreatureContext.lastTriggeredAbility = 1;
    }
    
    //*********** END INPUT FXNS **************************

    private void OnCrouch()
    {
        if(crouchModifier == 1f)
        {
            crouchModifier = .5f;
        }
        else
        {
            crouchModifier = 1f;
        }
    }

    private void OnFruitSpawn()
    {
        var temp = Instantiate(fruit, transform.position, Quaternion.identity);
        temp.GetComponent<Fruit>().droppedByPlayer = true;
    }
    
}
