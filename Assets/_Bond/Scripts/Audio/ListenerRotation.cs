//Enrico
//counters rotation of player for FMOD listener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerRotation : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.parent.rotation.y * -1.0f, 0);
    }
}
