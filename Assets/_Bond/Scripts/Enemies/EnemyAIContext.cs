// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAIContext : MonoBehaviour
{
    #region Behavior Tree Context

    [Header("Objects")]
    public GameObject player;
    public Transform enemyTransform;
    public Rigidbody rb;
    public EnemyData enemyData;
    public ActiveEnemyData activeEnemyData;
    public NavMeshAgent agent;
    public EnemyAnimator animator;
    
    [Header("Bools")]
    public bool isInPlayerRadius;
    public bool playerNoticedBefore;
    public bool playerNoticed;
    public bool isAbilityTriggered;
    public bool isIdling = false;
    public bool tookDamage = false;

    [Header("Misc.Numbers")]
    public float moveSpeed;
    public int lastTriggeredAbility;
    public float enemyDetectRange;
    public float wanderRadius; //how far from starting location the creature can wander
    public float wanderIdleDuration;
    public float wanderIdleTimer;
    public Vector3 wanderDestination;
    public Vector3 startingLocation;
    #endregion

    private void Awake()
    {
        enemyTransform = transform;
        startingLocation = enemyTransform.position;
        animator = GetComponent<EnemyAnimator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() 
    {

    }

    public void doMovement(float moveSpeed)
    {
        rb.velocity = (enemyTransform.transform.rotation * Vector3.forward * moveSpeed);
    }

    public void doRotation(float rotationSpeed, Quaternion desiredLook) 
    {
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, desiredLook, Time.deltaTime * rotationSpeed); //10 is rotation speed - might want to change later
    }

    public void doLookAt(Vector3 position)
    {
        enemyTransform.transform.LookAt(position, Vector3.up);
        rb.velocity = (enemyTransform.transform.rotation * Vector3.forward * moveSpeed);
    }
}
