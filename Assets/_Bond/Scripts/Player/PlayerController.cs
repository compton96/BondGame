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

        public bool usingMouse;
        public Vector2 mousePos;

    }
    public Inputs inputs;


    public Camera camera;
    public bool isoMovement = true;


    public GameObject fruit;
    public PlayerStateMachine fsm => GetComponent<PlayerStateMachine>();
    public PlayerAnimator animator => GetComponent<PlayerAnimator>();
    //public PlayerStats stats => GetComponent<PlayerStats>();
    public StatManager stats => GetComponent<StatManager>();
    public List<RelicStats> Relics = new List<RelicStats>();


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

    public int goldCount = 0;
    private float crouchModifier = 1;
    public bool nearInteractable = false;
    public bool hasSwapped;
    public bool inCombat;
    
    public Transform backFollowPoint;

    public GameObject wildCreature = null;
    public GameObject currCreature;
    public GameObject swapCreature;
    public GameObject interactableObject;
    public CreatureAIContext currCreatureContext;
    public CooldownSystem cooldownSystem => GetComponent<CooldownSystem>();

    public Dictionary<GameObject, InteractableBase> interactableObjects = new Dictionary<GameObject, InteractableBase>();
    public bool inCharacterDialog;
    public CharacterDialogManager characterDialogManager;

    
    //******Combat Vars**********//
    public bool isAttacking = false;
    public float currSpeed;
    public bool isHit;

    public ParticleSystem heavyChargeVfx;
    public ParticleSystem heavyHitVfx;
    public ParticleSystem slashVfx;
    public Vector3 destination;
    public Vector3 attackMoveVec;
    //****************//
    public float isoSpeedADJ = 0f;


    [Serializable]
    public struct HitBoxes
    {
        public GameObject slash0;
        public GameObject slash1;
        public GameObject slash2;
        public GameObject slash3;
        public GameObject slash4;
        public GameObject heavy;

    }

    public HitBoxes hitBoxes;

    public GameObject pauseMenu;
    private bool isPaused = false;



    // private void OnEnable()
    // {
    //     InputUser.onChange += OnInputDeviceChanged;
    // }

    // private void OnDisable()
    // {
    //     InputUser.onChange -= OnInputDeviceChanged; 
    // }
    
    // private void OnInputDeviceChanged(InputUser user, InputUserChange change, InputDevice device)
    // {
    //     Debug.Log("user : " + user  + " change : " + change + " device " + device);
    //     if (change == InputUserChange.ControlSchemeChanged) {

    //     }
    // }

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        dashStart = Time.time;
        animator.ResetAllAttackAnims();
        inputs.usingMouse = false;
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

        if(isDashing) 
        {   
            if(lastMoveVec == Vector3.zero) 
            {
                lastMoveVec = facingDirection;
            }
            movementVector = lastMoveVec * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
        }
        else if(isAttacking) 
        {   
            //moves player in direction of click
            movementVector = attackMoveVec * stats.getStat(ModiferType.MOVESPEED) * Time.deltaTime * movementModifier * crouchModifier;
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
        //Debug.Log(facingDirection);
        
    }

    
    private void OnMousePos(InputValue value)
    {
        //Debug.Log(value.Get<Vector2>());
        inputs.usingMouse = true;
        inputs.mousePos = value.Get<Vector2>();
    }
    
    
    //by Jamo
    private void OnInteract()
    {     
        if(inCharacterDialog)
        {
            if(characterDialogManager != null)
            {
                characterDialogManager.ContinueConvo();
            }
            
        }
        else if(interactableObjects.Count > 0)
        {
            InteractableBase tempBase = null;
            GameObject tempObj = null;
            float closestDist = 20;

            foreach(KeyValuePair<GameObject, InteractableBase> interactable in interactableObjects)
            {
                float tempDist = Vector3.Distance(interactable.Key.transform.position, gameObject.transform.position);
                if(tempDist < closestDist)
                {
                    closestDist = tempDist;
                    tempBase = interactable.Value;
                    tempObj = interactable.Key;
                }
            }

            tempBase.DoInteract();
            if(tempBase.removeOnInteract)
            {
                interactableObjects.Remove(tempObj);
            }
            if(interactableObjects.Count == 0)
            {
                PersistentData.Instance.UI.GetComponent<UIUpdates>().hideIntereactPrompt();
            }
        }

        //shop keeper (or any npc for dialog)
        //relics
        //creatures
        //chests
        //portal or something
        //lore items (walk up to thing and it gives you info etc)
        //fruit?


                
    }


    private void OnCreatureAutoAttack()
    {
        if(currCreatureContext != null)
        {
            if(currCreatureContext.autoAttack)
            {
                currCreatureContext.autoAttack = false;
            } 
            else 
            {
                currCreatureContext.autoAttack = true;
            }
            
        }
    }

    private void OnDash()
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

    public void befriendCreature()
    {
        bool requirementMet = true;
        if(requirementMet)
        {
            if(currCreature != null)
            {
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                swapCreature = wildCreature;
                swapCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);
                swapCreature.GetComponent<CreatureAIContext>().isActive = false;
                ApplyCreatureRelics(swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager);
            }
            else 
            { // first creature;
                wildCreature.GetComponent<CreatureAIContext>().isWild = false;
                currCreature = wildCreature;
                currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
                currCreatureContext.isActive = true;
                ApplyCreatureRelics(currCreatureContext.creatureStats.statManager);
            }

            // Debug.Log("BEFRIENDED");
            wildCreature.GetComponentInChildren<ParticleSystem>().Play();//PLAYS HEARTS, NEED TO CHANGE SO IT WORKS WITH MULTIPLE P-SYSTEMS
            PersistentData.Instance.UI.GetComponent<UIUpdates>().UpdateCreatureUI();
            InCombat(inCombat);

        }

    }

    public void ApplyCreatureRelics(StatManager _statManager)
    {
        foreach(RelicStats relic in Relics)
        {
            _statManager.AddRelic(relic.creatureModifiers);
        }
    }

    private void OnSwap()
    {
        if(swapCreature != null)
        {
            var temp = currCreature;
            currCreature.GetComponent<CreatureAIContext>().agent.Warp(Vector3.zero);
            swapCreature.GetComponent<CreatureAIContext>().agent.Warp(backFollowPoint.position);
            swapCreature.transform.position = backFollowPoint.position;
            currCreature = swapCreature;
            currCreatureContext = currCreature.GetComponent<CreatureAIContext>();
            currCreatureContext.isInPlayerRadius = false;
            currCreatureContext.isActive = true;
            swapCreature = temp;
            swapCreature.GetComponent<CreatureAIContext>().isActive = false;
            if (hasSwapped == false)
            {
                hasSwapped = true;
            }
            else
            {
                hasSwapped = false;
            }
            
            PersistentData.Instance.UI.GetComponent<UIUpdates>().UpdateCreatureUI();
        }
        

    }

    public void PutOnCD()
    {
        // Debug.Log(hasSwapped);
        if (hasSwapped)
        {
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility + 100;
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]);
            return;
        }
        else
        {
            // Debug.Log("New ID: " + currCreatureContext.lastTriggeredAbility);
            currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility].id = currCreatureContext.lastTriggeredAbility;
            cooldownSystem.PutOnCooldown(currCreatureContext.creatureStats.abilities[currCreatureContext.lastTriggeredAbility]);

        }
    }

    public void PutBasicOnCD()
    {
        if (hasSwapped)
        {
            currCreatureContext.basicCreatureAttack.id = 10 + 100;
            cooldownSystem.PutOnCooldown(currCreatureContext.basicCreatureAttack);
            return;
        }
        else
        {
            currCreatureContext.basicCreatureAttack.id = 10;
            cooldownSystem.PutOnCooldown(currCreatureContext.basicCreatureAttack);

        }
    }


    //Slash (X)
    private void OnAttack1()
    {
        // Debug.Log("attack");
        inputs.basicAttack = true;
        if(inputs.usingMouse)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(inputs.mousePos);
            int layerMask = 1 << 10;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                // Debug.Log("RAYCAST : " + hit.transform.gameObject);
                // Debug.Log(hit.point);
                //gameObject.transform.LookAt(hit.point);

                destination = hit.point;
                Vector3 direction = hit.point - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 9999f, 9999f);


                //Creates a movement vector for DoMovement to use on attacks. Moves player in direction of click
                Vector2 tempVec = new Vector2(newDirection.x,newDirection.z);
                tempVec.Normalize();
                attackMoveVec = new Vector3(tempVec.x, 0, tempVec.y);
                

                transform.rotation = Quaternion.LookRotation(new Vector3(newDirection.x, 0, newDirection.z));
                
                //Quaternion lookRotation = Quaternion.LookRotation(direction);
                //transform.forward = new Vector3(lookRotation.x, 0, lookRotation.z);
                
            } 
            //var dir = inputs.mousePos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
            //stinky
        }
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

    private void OnPause()
    {
        if(PersistentData.Instance.PauseMenu.GetComponent<Canvas>().enabled)
        {
            PersistentData.Instance.PauseMenu.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
        }
        else 
        {   
            PersistentData.Instance.PauseMenu.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }

        
    }
    
    //*********** END INPUT FXNS **************************

    private void OnCrouch()
    {
        // If standing, then crouch
        if(crouchModifier == 1f)
        {
            crouchModifier = .5f;
            animator.Crouch( true );
        }
        // If crouched, then stand up
        else
        {
            crouchModifier = 1f;
            animator.Crouch( false );
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
           PersistentData.Instance.LoadScene(1);
           stats.setStat(ModiferType.CURR_HEALTH, stats.getStat(ModiferType.MAX_HEALTH));
       }
       
    }

    public void InCombat(bool _inCombat)
    {
        inCombat = _inCombat;

        if(currCreature != null)
        {
            currCreatureContext.inCombat = inCombat;
            if(swapCreature != null)
            {
                swapCreature.GetComponent<CreatureAIContext>().inCombat = inCombat;
            }
        }
    }
    
}
