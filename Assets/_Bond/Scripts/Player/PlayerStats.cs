// Unknown and Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public PlayerController playerController => GetComponent<PlayerController>();

    [SerializeField] private float maxHealth = 500;

    //This is a property. Use MaxHealth when accessing 
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
            healthUIUpdate();
        }
    }
    public float maxHealthModAdd;
    public float maxHealthModMult;

    [SerializeField] private float currHealth;
    //This is a property. Use CurrHealth when accessing 
    public float CurrHealth
    {
        get
        {
            return currHealth;
        }
        set
        {
            currHealth = value;
            playerController.isHit = true;
            healthUIUpdate();
        }
    }
    public float currHealthModAdd;
    public float currHealthModMult;

    public float attack1Damage = 100;
    public float attack2Damage = 120;
    public float attack3Damage = 150;
    public float heavyDamage = 200;
    public float attackModAdd = 0;
    public float attackModMult = 0;

    public float speed;
    public float speedModAdd;
    public float speedModMult;

    public Slider slider;
    public TextMeshProUGUI maxHealthUI;
    public TextMeshProUGUI currHealthUI;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // updates health UI 
    void Update()
    {
       
    }

    void healthUIUpdate()
    {
        slider.value = (currHealth / maxHealth) * 100;
        currHealthUI.SetText((Mathf.Round(currHealth)).ToString());
        maxHealthUI.SetText("/ " + maxHealth.ToString());
    }

    public void takeDamage(float amount)
    {
        CurrHealth -= amount;
        if(CurrHealth <= 0)
        {
            //trigger loss
            Destroy(gameObject);
        }
    }


}
