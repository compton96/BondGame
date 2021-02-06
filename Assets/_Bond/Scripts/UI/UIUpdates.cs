using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdates : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI maxHealthUI;
    public TextMeshProUGUI currHealthUI;

    private StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void FixedUpdate() 
    {
        Debug.Log("I EXIST");
        //Probably change this to only get called on health changes for efficiency
        slider.value = (stats.getStat(ModiferType.CURR_HEALTH) / stats.getStat(ModiferType.MAX_HEALTH)) * 100;

        currHealthUI.SetText((Mathf.Round(stats.getStat(ModiferType.CURR_HEALTH))).ToString());
        maxHealthUI.SetText("/ " + stats.getStat(ModiferType.MAX_HEALTH).ToString());
    }
}
