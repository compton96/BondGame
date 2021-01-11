//by Jameson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public GameObject mainCam;
	private Transform thisCam;
	
	//Trauma level corresponds to how much shake there is. 
	//Hits or events that should shake the cam add trauma
    private float traumaMin = 0f, traumaMax = 1f;
	public float traumaLevel = 0f;
    private float shake = 0f;
    public float decreaseFactor = 0.5f;

	//rotational shake
    float yaw, maxYaw = 1f;
    float pitch, maxPitch = 0.5f;
    float roll, maxRoll = 1.2f;
	//translational shake
	float offsetX, offsetMax = 0.5f;
	float offsetY;
	float offsetZ;
	

	
	Vector3 originalPos;
	
	void Awake()
	{       
		thisCam = gameObject.transform;
	}

	void Update()
	{
		//shakey cam follow the original cam
		thisCam.rotation = mainCam.transform.rotation;
		thisCam.position = mainCam.transform.position;

		if(traumaLevel > 0) 
		{
			//switches to shakey cam view
			mainCam.GetComponent<Camera>().enabled = false;

			yaw = maxYaw * shake * Random.Range(-1f,1f);
			pitch = maxPitch * shake * Random.Range(-1f,1f);
			roll = maxRoll * shake * Random.Range(-1f,1f);

			offsetX = offsetMax * shake * Random.Range(-1f,1f);
			offsetY = offsetMax * shake * Random.Range(-1f,1f);
			offsetZ = offsetMax * shake * Random.Range(-1f,1f);

			thisCam.rotation *= Quaternion.Euler(yaw,pitch,roll);
			thisCam.position += new Vector3(offsetX,offsetY, offsetZ);
        
        	shake = traumaLevel * traumaLevel;
		}
		//trauma decreases linearly, maybe change to a set time frame?
        traumaLevel -= Time.deltaTime * decreaseFactor;
		traumaLevel = Mathf.Clamp(traumaLevel, traumaMin, traumaMax);
		//decrease trauma by factor each update

		if(traumaLevel == 0) mainCam.GetComponent<Camera>().enabled = true;
	}

	public void addTrauma(float trauma)
	{
		traumaLevel += trauma;
		traumaLevel = Mathf.Clamp(traumaLevel, traumaMin, traumaMax);
	}

}
