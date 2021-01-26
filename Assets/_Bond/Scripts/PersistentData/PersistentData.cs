using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{

    private static PersistentData _instance;
    public static PersistentData Instance { get { return _instance; } }

    //References to things that need to be easily accessible
    [Header("Reference")]
    public GameObject PlayerPrefab;
    public GameObject Player { get; private set; }
    private GameObject player;



    private void Awake() 
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
    }
    

    private void Init() 
    {
        if(player == null){
            try
            {
                Player = GameObject.FindGameObjectWithTag("Player");
            }
            catch
            {
                Player = Instantiate(PlayerPrefab, GetSpawnpoint(), Quaternion.identity);
            }
            
        }
        Camera.main.GetComponent<CamFollow>().toFollow = player.transform;
    }


    public void LoadScene(int _scene)
    {
        Transition(_scene);
    }


    IEnumerator Transition(int _scene) 
    {
        AsyncOperation loadNewScene;
        try 
        {
            loadNewScene = SceneManager.LoadSceneAsync(_scene);
        } 
        catch 
        {
            Debug.LogError("COULD NOT LOAD SCENE WITH NAME :" + _scene);
            yield break;
        }

        //probably want to make the game "pause" so you cant move or die

        //make child everything we want to keep
        MakeChild(Player);
        MakeChild(Player.GetComponent<PlayerController>().currCreature);
        MakeChild(Player.GetComponent<PlayerController>().swapCreature);
        //Loading Scene, can make transition stuff here
        /* for example, some screen fading stuff : 
            //transition OUT
            yield return StartCoroutine(FadeLoadingScreen(1, 1));
        */
        while (!loadNewScene.isDone)
        {
            //update some slider to show = loadNewScene.progress
            yield return null;
        }
        //unmake all children
        UnmakeChild(Player);
        UnmakeChild(Player.GetComponent<PlayerController>().currCreature);
        UnmakeChild(Player.GetComponent<PlayerController>().swapCreature);

        //set players position in new scene
        //CALL BUILD LEVEL, WHICH SHOULD GENERATE EVERYTHING, INCLUDING A SPAWNPOINT;
        Player.transform.position = GetSpawnpoint();
        Player.GetComponent<PlayerController>().currCreature.transform.position = Player.GetComponent<PlayerController>().backFollowPoint.transform.position;
        /*
            //transition IN
            yield return StartCoroutine(FadeLoadingScreen(0, 1));
        */


    }

    private Vector3 GetSpawnpoint(){
        Vector3 spawnpoint;
        try
        {
            spawnpoint = GameObject.FindGameObjectWithTag("spawnpoint").transform.position;
        } 
        catch 
        {   
            Debug.LogError("NO SPAWNPOINT SET IN THIS SCENE");
            spawnpoint = Vector3.zero;
        }
        return spawnpoint;
    }

    private void MakeChild(GameObject _gameObject)
    {
        _gameObject.transform.parent = gameObject.transform;
    }

    private void UnmakeChild(GameObject _gameObject)
    {
        _gameObject.transform.parent = null;
    }
}
