using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter masterMusicEvent;
    public FMODUnity.StudioEventEmitter ambientNoiseEvent;
    private IEnumerator fanfareEnd;
    private float waitTime;

    private void Start()
    {
        fanfareEnd = EndCombatMusicFanfare();
        //wait for one bar before resetting Game State
        waitTime = 4f / 136f;
    }

    private void Update()
    {
       
    }

    /*
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
    */

    public void BeginFarmMusic()
    {
        masterMusicEvent.SetParameter("Game State", 0);
    }

    public void BeginOverworldMusic()
    {
        masterMusicEvent.SetParameter("Game State", 1);
    }

    public void BeginCombatMusic()
    {
        masterMusicEvent.SetParameter("Game State", 2);
    }

    public void BeginCombatMusicFanfare()
    {
        masterMusicEvent.SetParameter("Game State", 3);
    }

    private IEnumerator EndCombatMusicFanfare()
    {
        yield return new WaitForSeconds(waitTime);
        masterMusicEvent.SetParameter("Game State", 1);
    }
}
