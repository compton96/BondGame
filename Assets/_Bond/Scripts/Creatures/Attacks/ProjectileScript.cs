using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    GameObject target;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    float speed = 10;
    public float damage = 50;
    public bool isHoming = false;

    void Start()
    {
        
    }

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(isHoming) 
            {
                transform.LookAt(target.transform.position);
            }
            
            rigidBody.velocity = (transform.rotation*Vector3.forward*speed);
        }
    }

    public void setTarget(GameObject _target, float _speed, float _damage, bool _isHoming)
    {
        target = _target;
        transform.LookAt(target.transform.position);
        speed = _speed;
        damage = _damage;
        isHoming = _isHoming;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyAIContext>().statManager.takeDamage(damage, ModiferType.RANGED_RESISTANCE);
            Destroy(gameObject);
        }    
    }
}
