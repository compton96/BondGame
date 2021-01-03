//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public bool isoMovement = true;

    public GameObject fruit;
    public PlayerStateMachine playerStateMachine => GetComponent<PlayerStateMachine>();
    public PlayerAnimator animator => GetComponent<PlayerAnimator>();

    public bool isAttackAnim = false; 
    public bool isFollowThroughAnim = false;

    public float speed = 1f;
    public float turnSpeed = 0.15f;

    public float dashSpeed;
    public float dashTime;
    public float dashDelay = 1.2f;
    public float dashStart = 2;
    public int dashCount = 0;
    public bool isDashing = false;

    Rigidbody rb;
    Vector2 movementVel;
    CharacterController charController;
    public Vector3 v3Vel;
    Vector3 gravity;

    public float crouchModifier = 1;
    public bool playerBasicAttack;
    public bool playerDash;
    public bool playerInteract;
    public bool playerHeavyAttack;
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

    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        dashStart = Time.time;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //character controller 
        //doMovement(1f);
        //doRotation(1f);
        //manual movement
        // rb.velocity = new Vector3(movementVel.x * speed, rb.velocity.y, movementVel.y * speed );
        // // Quaternion lookRot = new Quaternion(Quaternion.LookRotation(movementVel).x ,Quaternion.LookRotation(movementVel).y ,0, Quaternion.LookRotation(movementVel).w);
        // // transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, turnSpeed);
        // if(rb.velocity != Vector3.zero)
        //     transform.rotation = Quaternion.LookRotation(rb.velocity);
    }


    public void doMovement(float movementModifier){
        
        if(!isDashing)
        {
            v3Vel = new Vector3(movementVel.x, 0, movementVel.y);
            
            if(isoMovement){
                v3Vel = Quaternion.Euler(0, -45, 0) * v3Vel;
            }
        }
       
      
        

        if(!charController.isGrounded) {
            gravity += Physics.gravity * Time.deltaTime;
        } else{
            gravity = Vector3.zero;
        }
        currSpeed = (Mathf.Abs(v3Vel.x) + Mathf.Abs(v3Vel.z)) / 2 * speed * Time.deltaTime * movementModifier * crouchModifier;
        var movementVector = v3Vel * speed * Time.deltaTime * movementModifier * crouchModifier;
        charController.Move(movementVector);
        charController.Move(gravity * Time.deltaTime);

    

        animator.Move(movementVector);
    }

    public void doRotation(float rotationModifier){
        v3Vel = new Vector3(movementVel.x, 0, movementVel.y);

        

        if(movementVel != Vector2.zero) {
            if(isoMovement){
                v3Vel = Quaternion.Euler(0, -45, 0) * v3Vel;
            }
            transform.forward = Vector3.Slerp(transform.forward, v3Vel, Time.deltaTime * turnSpeed * rotationModifier);
        }
    }

    public void setRotation(Vector3 vec)
    {
        transform.forward = vec;
    }

    void OnMovement(InputValue value){
        //Debug.Log(value.Get<Vector2>());
        movementVel = value.Get<Vector2>();
        movementVel.Normalize();
        
    }

    //by Jamo
    //allow for no more than two dashes in rapid succession
    void OnInteract(){ //pressing dash button       
        if(nearInteractable) {
            playerInteract = true;
            if(interactableObject != null){
                Debug.Log("picked up item");
                Destroy(interactableObject);
                nearInteractable = false;
            }
            else if (wildCreature != null) {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                currCreature = wildCreature;
                currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
                //FIX LATER --- NEED TO DISABLE NOTICE/INTERACT COLLIDERS
                nearInteractable = false;
            }
        } else {
            if(Time.time > dashStart + dashDelay)//cant dash until more time than dash delay has elapsed,
            {
                //takes dash start time
                dashStart = Time.time;
                dashCount++;
                playerDash = true;
            }
            else if(dashCount >= 1 )//if you have dashed once and are not past delay, you can dash a second time
            {
                dashCount = 0;
                playerDash = true;          
                
            }   
        }
                 
    }

    void OnFruitSpawn(){
        var temp = Instantiate(fruit, transform.position, Quaternion.identity);
        temp.GetComponent<Fruit>().droppedByPlayer = true;
    }

    void OnAttack1(){
        playerBasicAttack = true;
    }

    void OnHeavyAttack(InputValue value){
        
        float val = value.Get<float>();

        if(val == 1) playerHeavyAttack = true;
        else playerHeavyAttack = false;
        
    }


    void OnAttack2(){
        currCreatureContext.isAbilityTriggered = true;
        currCreatureContext.lastTriggeredAbility = 0;
    }  
    void OnAttack3(){
        currCreatureContext.isAbilityTriggered = true;
        currCreatureContext.lastTriggeredAbility = 1;
    }



    public bool getIsAttackAnim()
    {
        return isAttackAnim;
    }

    public void setIsAttackAnim(bool state)
    {
        isAttackAnim = state;
    }

    public bool getIsFollowThroughAnim()
    {
        return isFollowThroughAnim;
    }

    public void setIsFollowThroughAnim(bool state)
    {
        isFollowThroughAnim = state;
    }

    void OnCrouch() {
        if(crouchModifier == 1f){
            crouchModifier = .5f;
        } else {
            crouchModifier = 1f;
        }
    }
    
}
