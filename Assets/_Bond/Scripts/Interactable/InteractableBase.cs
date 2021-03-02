using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public bool showUI;
    public bool removeOnInteract;
    public abstract void DoInteract();


}
