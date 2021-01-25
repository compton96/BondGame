using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

[System.Serializable]
public class CreatureAIContext : MonoBehaviour
{
    #region Behavior Tree Context

    [Header("Objects")]
    public GameObject player;
    public GameObject targetEnemy;
    public GameObject cleverItem; //interesting items, only for clever creatures
    public GameObject foundFood; //Used in Hearty personality to find food
    public Transform creatureTransform;
    public Rigidbody rb;
    public ActiveCreatureData CD;
    public creatureData creatureTypeData;
    public NavMeshAgent agent;
    public GameObject backFollowPoint;
    public GameObject followPoint;
    public GameObject projectileSpawner;
    public CreatureAnimator animator;
    public Collider interactRadius;
    public CooldownSystem cooldownSystem;
    

    
    [Header("Bools")]
    public bool isWild;
    public bool isInPlayerRadius;
    public bool isInPlayerTrail;
    public bool isNoticed;
    public bool isAbilityTriggered;
    public bool wanderIdling = false;
    public bool cleverIgnoreItems = false;

    [Header("Misc.Numbers")]
    public float playerSpeedToScare;
    public int lastTriggeredAbility;
    public float enemyDetectRange;
    public float itemDetectRange; //range for detecting interesting items, only for clever creatures
    public float wanderRadius; //how far from starting location the creature can wander
    public float wanderIdleDuration;
    public float wanderIdleTimer;
    public Vector3 wanderDestination;
    public Vector3 wildStartingLocation;
    public float stealDuration;
    public float stealTimer;


    private int debugNumber;
    private CreatureDebugText debugText;
    #endregion

    private void Awake()
    {
        creatureTransform = transform;
        wildStartingLocation = creatureTransform.position;
        animator = GetComponent<CreatureAnimator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        followPoint = GameObject.FindGameObjectWithTag("FrontFollowPoint");
        backFollowPoint = GameObject.FindGameObjectWithTag("BackFollowPoint");
        // GameObject temp = GameObject.FindGameObjectWithTag("CreatureDebugText");
        // debugText = temp.GetComponent<CreatureDebugText>();
        // debugText.creaturesDebug.Add("");
        // debugNumber = debugText.creaturesDebug.Count - 1;
        if(isWild){
            lastTriggeredAbility = 0;
        }

        resetStealTimer();
    }

    public void GetActiveCreatureData(){
        CD = GetComponent<ActiveCreatureData>();
    }

    private void FixedUpdate() {

    }


    public void doMovement(float moveSpeed){
        rb.velocity = (creatureTransform.transform.rotation * Vector3.forward * moveSpeed);
    }

    public void doRotation(float rotationSpeed, Quaternion desiredLook) {
        creatureTransform.rotation = Quaternion.Slerp(creatureTransform.rotation, desiredLook, Time.deltaTime * rotationSpeed); //10 is rotation speed - might want to change later
    }

    public void doLookAt(Vector3 position){
        creatureTransform.transform.LookAt(position, Vector3.up);
        rb.velocity = (creatureTransform.transform.rotation * Vector3.forward * CD.moveSpeed);
    }


    public void resetStealTimer() {
        stealTimer = 0;
        stealDuration = Random.Range(2f, 3f);
    }
    
    public void updateDebugText(string name) {
        // debugText.creaturesDebug[debugNumber] = gameObject.name + " : " + name + "\n";
    }


}
