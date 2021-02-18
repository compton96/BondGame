using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public int sceneIndex;
    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log("entered");
        if(other.transform.tag == "Player")
        {
            // Debug.Log("try load");
            PersistentData.Instance.LoadScene(sceneIndex);
        } 
    }
}
