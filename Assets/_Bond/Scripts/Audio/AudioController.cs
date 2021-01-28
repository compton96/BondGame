using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter masterMusicEvent;
    public FMODUnity.StudioEventEmitter ambientNoiseEvent;

    public Transform playerTransform;
    private bool inCombat = false;
    public float enemyDetectRange;
    private int layerMask = 1 << 8; //to target only layer 8 - enemies


    private void Update()
    {
        //check if player is near enemies
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, enemyDetectRange, layerMask);

        //enemy found, prep combat music
        if (hitColliders.Length > 0)
        {
            //if not yet in combat, transition to combat music
            if (!inCombat)
            {
                masterMusicEvent.SetParameter("Combat State", 1);
                masterMusicEvent.SetParameter("Song 2 - State", 0);
                enemyDetectRange *= 4f;
                inCombat = true;
            }
            else 
            {
                //already in combat music
                Collider[] nearHitCollider = Physics.OverlapSphere(playerTransform.position, enemyDetectRange, layerMask);
                if (nearHitCollider.Length > 0)
                {
                    masterMusicEvent.SetParameter("Song 2 - State", 1);
                }
            }
        } 
        //no enemies found
        else
        {
            //if in combat, transition out of combat music
            if (inCombat)
            {
                masterMusicEvent.SetParameter("Combat State", 0);
                masterMusicEvent.SetParameter("Song 2 - State", 2);
                enemyDetectRange /= 4f;
                inCombat = false;
            }
        }
    }
}
