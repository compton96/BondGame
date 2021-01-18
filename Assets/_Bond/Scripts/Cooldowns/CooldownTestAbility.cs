using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTestAbility : MonoBehaviour , HasCooldown
{
    [SerializeField] private CooldownSystem cooldownSystem = null;

    [SerializeField] private int id = 1;
    [SerializeField] private float cooldownDuration = 5f;

    public int Id=>id;
    public float CooldownDuration => cooldownDuration;
    private void Update()
    {
        // if (!Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     return;
        // }
    }
}
