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
    public CanvasGroup loadScreen;



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
        if(Player == null){
            try
            {
                Player = GameObject.FindGameObjectWithTag("Player");
                if(Player == null)
                {
                    Player = Instantiate(PlayerPrefab, GetSpawnpoint(), Quaternion.identity);
                }
            }
            catch
            {
                Player = Instantiate(PlayerPrefab, GetSpawnpoint(), Quaternion.identity);
            }
            
        }
        Camera.main.GetComponent<CamFollow>().toFollow = Player.transform;
    }


    public void LoadScene(int _scene)
    {
        StartCoroutine(Transition(_scene));
    }


    IEnumerator Transition(int _scene) 
    {
        PlayerController playerController = Player.GetComponent<PlayerController>();

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
        MakeChild(playerController.currCreature);
        MakeChild(playerController.swapCreature);
        //Loading Scene, can make transition stuff here
         //for example, some screen fading stuff : 
            //transition OUT
        yield return StartCoroutine(FadeInScreen(0.5f));
        
        
        while (!loadNewScene.isDone)
        {
            //update some slider to show = loadNewScene.progress
            yield return null;
        }

        
        //unmake all children
        UnmakeChild(Player);
        UnmakeChild(playerController.currCreature);
        UnmakeChild(playerController.swapCreature);

        //set players position in new scene
        //CALL BUILD LEVEL, WHICH SHOULD GENERATE EVERYTHING, INCLUDING A SPAWNPOINT;

        
        Debug.Log(GetSpawnpoint());
        Player.transform.position = GetSpawnpoint();
       


        if( playerController.currCreature != null)
        {
            playerController.currCreature.transform.position = 
                    playerController.backFollowPoint.transform.position;    
               
            // playerController.currCreature.GetComponent<CreatureAIContext>().agent.ResetPath();
            // playerController.currCreature.GetComponent<CreatureAIContext>().agent.SetDestination(playerController.backFollowPoint.transform.position);
             playerController.currCreature.GetComponent<CreatureAIContext>().agent.Warp(playerController.backFollowPoint.transform.position);
        }
        
        /*
            //transition IN
            yield return StartCoroutine(FadeLoadingScreen(0, 1));
        */
        yield return StartCoroutine(FadeOutScreen(1));


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
        if(_gameObject != null)
        {
            _gameObject.transform.parent = gameObject.transform;
        }
        
    }

    private void UnmakeChild(GameObject _gameObject)
    {
        if(_gameObject != null)
        {
           _gameObject.transform.parent = null;
           SceneManager.MoveGameObjectToScene(_gameObject,SceneManager.GetActiveScene());
        }
        
    }

    IEnumerator FadeInScreen(float duration)
    {
        float startValue = loadScreen.alpha;
        float time = 0;

        while (time < duration)
        {
            loadScreen.alpha = Mathf.Lerp(startValue, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        loadScreen.alpha = 1;
    }

    IEnumerator FadeOutScreen(float duration)
    {
        float startValue = loadScreen.alpha;
        float time = 0;

        while (time < duration)
        {
            loadScreen.alpha = Mathf.Lerp(startValue, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        loadScreen.alpha = 0;
    }
}
