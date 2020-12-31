// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    [SerializeField]
    private Fruit newFruit;
    [SerializeField]
    private GameObject fruitSpawnLocation;

    public void dropFruit()
    {
        Instantiate(newFruit, fruitSpawnLocation.transform.position, Quaternion.identity);
    }
}
