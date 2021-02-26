using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter masterMusicEvent;
    public FMODUnity.StudioEventEmitter ambientNoiseEvent;


    private void Update()
    {
       
    }

    public void BeginCombatMusic()
    {
        masterMusicEvent.SetParameter("Combat State", 1);
        masterMusicEvent.SetParameter("Song 2 - State", 1);
    }

    public void EndCombatMusic()
    {
        masterMusicEvent.SetParameter("Combat State", 0);
        masterMusicEvent.SetParameter("Song 2 - State", 2);
    }
}
