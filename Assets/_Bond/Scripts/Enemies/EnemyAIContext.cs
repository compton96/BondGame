// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class EnemyAIContext : MonoBehaviour
{
    #region Behavior Tree Context

    //[Header("Main Stats")]
    public StatManager statManager => GetComponent<StatManager>();
    // public float maxHealth;
    // public float currentHealth;
    // public int damage;
    // public float moveSpeed;

    [Header("Objects")]
    public GameObject player;
    public Transform enemyTransform;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public EnemyAnimator animator;

    public Canvas enemyUI;
    public Slider healthSlider;
    public ParticleSystem hitVFX;
    
    [Header("Bools")]
    public bool isInPlayerRadius;
    public bool playerNoticedBefore;
    public bool playerNoticed;
    public bool isIdling = false;
    public bool tookDamage = false;

    [Header("Misc.Numbers")]
    // public int lastTriggeredAbility;
    // public float enemyDetectRange;
    // public float wanderRadius; //how far from starting location the creature can wander
    // public float wanderIdleDuration;
    // public float wanderIdleTimer;
    // public Vector3 wanderDestination;
    public float delayBetweenAttacks;
    public Vector3 startingLocation;
    private float lastCheckedHealth;
    public float lastDamageTaken;
    public GameObject goldPrefab;
    #endregion

    private void Awake()
    {
        enemyTransform = transform;
        startingLocation = enemyTransform.position;
        animator = GetComponent<EnemyAnimator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        // player = GameObject.FindGameObjectWithTag("Player");
        // lastCheckedHealth = statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;
    }

    private void Start() {
        //player = PersistentData.Instance.Player;
        player = GameObject.FindGameObjectWithTag("Player");
        lastCheckedHealth = statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;
    }

    private void FixedUpdate() 
    {
        //Check for damage and update health UI accordingly
        if(statManager.stats[ModiferType.CURR_HEALTH].modifiedValue < lastCheckedHealth)
        {
            tookDamage = true;
            lastDamageTaken = lastCheckedHealth - statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;
            healthUIUpdate();
            lastCheckedHealth = statManager.stats[ModiferType.CURR_HEALTH].modifiedValue;

            hitVFX.Play();
        }

        enemyUI.transform.rotation = Camera.main.transform.rotation; //makes ui always face camera

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
        rb.velocity = (enemyTransform.transform.rotation * Vector3.forward * statManager.stats[ModiferType.MOVESPEED].modifiedValue);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void healthUIUpdate()
    {
        healthSlider.value = (statManager.stats[ModiferType.CURR_HEALTH].modifiedValue / statManager.stats[ModiferType.MAX_HEALTH].modifiedValue) * 100;
    }

    public void dropGold()
    {
        Vector3 spawnPos = new Vector3(enemyTransform.position.x, 0.2f , enemyTransform.position.z);
        Instantiate(goldPrefab, spawnPos, Quaternion.identity);
    }
}
