//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    //public PlayerStats stats => GetComponent<PlayerStats>();
    public StatManager stats => GetComponent<StatManager>();

    //*******Dash Variables*******
    private float dashStart = 2;
    private int dashCount = 0;

    [HideInInspector]
    public Vector3 facingDirection;
    [HideInInspector]
    public bool isDashing = false;
    [HideInInspector]
    public Vector3 lastMoveVec;
    [HideInInspector]
    public Vector3 movementVector;
    //****************************

    private Rigidbody rb;
    
    private CharacterController charController;
    
    private Vector3 gravity;

    private float crouchModifier = 1;
    public bool nearInteractable = false;
    public bool hasSwapped;
    
    public Transform backFollowPoint;

    public GameObject wildCreature = null;
    public GameObject currCreature;
    public GameObject swapCreature;
    public GameObject interactableObject;
    public CreatureAIContext currCreatureContext;
    public CooldownSystem cooldownSystem => GetComponent<CooldownSystem>();

    
    //******Combat Vars**********//
    public bool isAttacking = false;
    public float currSpeed;
    public bool isHit;

    public ParticleSystem heavyChargeVfx;
    public ParticleSystem heavyHitVfx;
    public ParticleSystem slashVfx;
    //****************//
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

        if(isDashing || isAttacking) 
        {   
            if(lastMoveVec == Vector3.zero) 
            {
                lastMoveVec = facingDirection;
            }
            movementVector = lastMoveVec * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
        }
        else 
        {
            movementVector = inputs.moveDirection * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
            lastMoveVec = inputs.moveDirection;
        }
        
        charController.Move(movementVector);
        charController.Move(gravity * Time.deltaTime);
        animator.Move(movementVector);


        
    }

    public void doRotation(float rotationModifier)
    {
        if(inputs.rawDirection != Vector2.zero)
        {
            if(isAttacking)
            {//CHANGE LATER, DONT HARDCODE TURN SPEED AS 14
                 transform.forward = Vector3.Slerp(transform.forward, lastMoveVec, Time.deltaTime * 14f * rotationModifier);
            }
            else
            {
                transform.forward = Vector3.Slerp(transform.forward, inputs.moveDirection, Time.deltaTime * 14f * rotationModifier);
            }
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
            inputs.moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * inputs.moveDirection;
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
                //DO PICKUP LOGIC, ADDING ITEM TO CORRECT LOCATION ETC;
                Destroy(interactableObject);
                nearInteractable = false;
            }
            else if (wildCreature != null)
            {
                befriendCreature();
            }
        }
        else//Not near interactable, dash instead
        {
            if(Time.time > dashStart + stats.getStat(ModiferType.DASH_COOLDOWN))//cant dash until more time than dash delay has elapsed,
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

    private void befriendCreature()
    {
        bool requirementMet = true;
        if(requirementMet)
        {
            if(currCreature != null)
            {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                swapCreature = wildCreature;
                swapCreature.SetActive(false);
                //FIX LATER --- NEED TO DISABLE NOTICE/INTERACT COLLIDERS
                nearInteractable = false;
            }
            else 
            {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                currCreature = wildCreature;
                currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
                //FIX LATER --- NEED TO DISABLE NOTICE/INTERACT COLLIDERS
                nearInteractable = false;
            }

        }

    }

    private void OnSwap()
    {
        if(swapCreature != null)
        {
            var temp = currCreature;
            currCreature.SetActive(false);
            swapCreature.SetActive(true);
            swapCreature.transform.position = backFollowPoint.position;
            currCreature = swapCreature;
            currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
            currCreatureContext.isInPlayerRadius = false;
            swapCreature = temp;
            if (hasSwapped == false)
            {
                hasSwapped = true;
            }
            else
            {
                hasSwapped = false;
            }
        }

    }

    public void PutOnCD()
    {
        Debug.Log(hasSwapped);
        if (hasSwapped)
        {
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility + 100;
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]);
            return;
        }
        else
        {
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility;
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]);
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
        // var id = currCreatureContext.CD.abilities[0].id;
        // var cooldownDuration = currCreatureContext.CD.abilities[0].cooldownDuration;

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

    public void DeathCheck(){
       if(stats.getStat(ModiferType.CURR_HEALTH) <= 0)
       {
           PersistentData.Instance.LoadScene(0);
           stats.setStat(ModiferType.CURR_HEALTH, stats.getStat(ModiferType.MAX_HEALTH));
       }
       
    }


    
}
