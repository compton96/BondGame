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
    public Slider enthusiasmSlider;

    private StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();
    private PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateCreatureUI();
    }


    
    private void FixedUpdate() 
    {
        //Probably change this to only get called on health changes for efficiency
        slider.value = (stats.getStat(ModiferType.CURR_HEALTH) / stats.getStat(ModiferType.MAX_HEALTH)) * 100;

        currHealthUI.SetText((Mathf.Round(stats.getStat(ModiferType.CURR_HEALTH))).ToString());
        maxHealthUI.SetText("/ " + stats.getStat(ModiferType.MAX_HEALTH).ToString());
        gold.SetText(player.goldCount.ToString());

        /*
        
        if a cooldown is active
        {
            somewhere on cooldown activation, set fillAmount to 0
            get active cooldowns (max of 4)
            CooldownUpdate(image) for each active
        }
        */

        
    }

   

    public void CooldownUpdate()
    {
        //called every tick while cooldown is active
        //get specific creatures cooldown

        //Image.fillAmount += 1.0f / cooldown length * Time.deltaTime;

    }
	

	


    public void UpdateCreatureUI()
    {
         if(player.currCreatureContext != null)
        {
            enthusiasmSlider.enabled = true;
            updateEnthusiasm();
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
            enthusiasmSlider.enabled = false;
            currCreatureIcon.sprite = noCreatureIcon;
            swapCreatureIcon.sprite = noCreatureIcon;
            abilityGroup.alpha = 0;

        }
    }

    public void updateEnthusiasm()
    {
        var creatureStats = player.currCreatureContext.creatureStats.statManager;
        enthusiasmSlider.value = ((creatureStats.getStat(ModiferType.CURR_ENTHUSIASM) / creatureStats.getStat(ModiferType.MAX_ENTHUSIASM)) * 100);
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
