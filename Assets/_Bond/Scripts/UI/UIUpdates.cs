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
    public TextMeshProUGUI interactPrompt;

    public TextMeshProUGUI gold;

    public Image currCreatureIcon;
    public Image swapCreatureIcon;   
    public Sprite noCreatureIcon;

    public CanvasGroup abilityGroup;
    public Image ability1Icon;
    public Image ability2Icon;

    private StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();
    private PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();

    // Start is called before the first frame update
    void Start()
    {
        updateCreatureUI();
    }

    
    private void FixedUpdate() 
    {
        //Probably change this to only get called on health changes for efficiency
        slider.value = (stats.getStat(ModiferType.CURR_HEALTH) / stats.getStat(ModiferType.MAX_HEALTH)) * 100;

        currHealthUI.SetText((Mathf.Round(stats.getStat(ModiferType.CURR_HEALTH))).ToString());
        maxHealthUI.SetText("/ " + stats.getStat(ModiferType.MAX_HEALTH).ToString());
        gold.SetText(player.goldCount.ToString());
        
       
        
    }


    public void updateCreatureUI()
    {
         if(player.currCreatureContext != null)
        {

            currCreatureIcon.sprite = player.currCreatureContext.icon;
            abilityGroup.alpha = 1;
            ability1Icon.sprite = player.currCreatureContext.creatureStats.abilities[0].abilityIcon;
            ability2Icon.sprite = player.currCreatureContext.creatureStats.abilities[1].abilityIcon;

            if(player.swapCreature != null)
            {
                swapCreatureIcon.sprite = player.swapCreature.GetComponent<CreatureAIContext>().icon;
            }

        }
        else
        {
            currCreatureIcon.sprite = noCreatureIcon;
            swapCreatureIcon.sprite = noCreatureIcon;
            abilityGroup.alpha = 0;

        }
    }

    public void showInteractPrompt()
    {
        interactPrompt.enabled = true;
    }

    public void hideIntereactPrompt()
    {
        interactPrompt.enabled = false;
    }



}
