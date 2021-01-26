//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform toFollow;
    public float smoothSpeed = 0.125f; 


    void Start()
    {
        toFollow = PersistentData.Instance.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate ()
	{
		Vector3 desiredPosition = toFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

    private void LateUpdate() {
        // Vector3 desiredPosition = toFollow.position + offset;
		// Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		// transform.position = smoothedPosition;
    }
}
