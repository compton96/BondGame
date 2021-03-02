using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiFunctions : MonoBehaviour
{
   public void TransitionScene(int index)
   {
       SceneManager.LoadScene(index);
   }

   public void LoadSceneNoPersist(int index)
   {
       SceneManager.LoadScene(index);
   }

   public void SetTimeScale(int index)
   {
       Time.timeScale = index;
   }

   public void ExitGame()
   {
        Application.Quit();
   }
}
