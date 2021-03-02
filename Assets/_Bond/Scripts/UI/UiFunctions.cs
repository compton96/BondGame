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

   public void ExitGame()
   {
        Application.Quit();
   }
}
