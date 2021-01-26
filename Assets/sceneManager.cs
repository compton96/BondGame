using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public int sceneIndex;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            PersistentData.Instance.LoadScene(sceneIndex);
        } 
    }
}
