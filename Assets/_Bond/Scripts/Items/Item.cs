using UnityEngine;
using System.Collections;

//Eugene

[System.Serializable]                         //    Our Representation of an InventoryItem
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item  : ScriptableObject
{
    public string itemName = "New Item";      //    What the item will be called in the inventory
    public GameObject prefab;
    public Texture2D itemIcon = null;         //    What the item will look like in the inventory
    public Rigidbody itemObject = null;       //    Optional slot for a PreFab to instantiate when discarding
    public bool isUnique = false;             //    Optional checkbox to indicate that there should only be one of these items per game
    public bool isIndestructible = false;     //    Optional checkbox to prevent an item from being destroyed by the player (unimplemented)
    public bool isQuestItem = false;          //    Examples of additional information that could be held in InventoryItem
    public bool isStackable = false;
    public bool creatureFood = false;

    public bool destroyOnUse = false;         // do we want it to be destroyed?
    public float encumbranceValue = 0;        //    Examples of additional information that could be held in InventoryItem  !!!
    public Vector3 spawnPoints;
}