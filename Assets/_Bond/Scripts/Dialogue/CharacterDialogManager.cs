using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDialogManager : MonoBehaviour
{
    public List<Conversation> conversations = new List<Conversation>();


    Conversation currConversation;
    
    string currString;
    
    bool waitForContinue;

    TextMeshProUGUI dialogueBox;


    private float textSpeed; //speed for text
    private float textSpeedMult; //for speeding up text



    public void StartConvo()
    {

        PersistentData.Instance.Player.GetComponent<PlayerController>().inCharacterDialog = true;
        textSpeed = 0.0375f;

        currConversation = conversations[Random.Range(0,conversations.Count)];

        currConversation.step = 0;
        PersistentData.Instance.UI.GetComponent<UIUpdates>().ShowCharacterDialogue();
        dialogueBox = PersistentData.Instance.UI.GetComponent<UIUpdates>().CharacterDialogText;
        dialogueBox.text = "";
        currString = currConversation.dialog[currConversation.step];
        StartCoroutine("LetterByLetter", currString);
        waitForContinue = false;

    }

    public void ContinueConvo()
    {
        if(waitForContinue){
            currConversation.step++;
            if(currConversation.step >= currConversation.dialog.Count)
            {
                EndConvo();
            } 
            else
            {
                currString = currConversation.dialog[currConversation.step];
                dialogueBox.text = "";
                StartCoroutine("LetterByLetter", currString);
                waitForContinue = false;
            }
        }
        else
        {
            StopCoroutine("LetterByLetter");
            dialogueBox.text = currString;
            waitForContinue = true;
        }
    }

    public void EndConvo()
    {
        PersistentData.Instance.UI.GetComponent<UIUpdates>().HideCharacterDialogue();
        PersistentData.Instance.Player.GetComponent<PlayerController>().inCharacterDialog = false;
        PersistentData.Instance.Player.GetComponent<PlayerController>().characterDialogManager = null;
    }

    public IEnumerator LetterByLetter(string sentence)
    {
        while (sentence.Length > 0)
        {
            string currLetter = sentence.Substring(0, 1);
            sentence = sentence.Substring(1);
            dialogueBox.text += currLetter;
            yield return new WaitForSeconds(textSpeed);
        }
        waitForContinue = true;

    }


}
