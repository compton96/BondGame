// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Item item;
    
    Fruit(bool playerDropped)
    {
        droppedByPlayer = playerDropped;
    }

    public bool droppedByPlayer = false;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
