using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CreatureDebugText : MonoBehaviour
{
    public List<string> creaturesDebug = new List<string>();
    public TextMeshProUGUI TextBox;
    
    private void Awake() {
        TextBox = GetComponent<TextMeshProUGUI>();
    }


    private void Update() {
        TextBox.text = "";
        foreach(string s in creaturesDebug) {
            TextBox.text += s + "\n";
        }
    }
}
