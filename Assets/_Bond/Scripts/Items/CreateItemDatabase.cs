using UnityEngine;
using System.Collections;
using UnityEditor;

//Eugene

public class CreateItemDatabase 
{
    [MenuItem("Assets/Create/Inventory Item List")]
    public static ItemDatabase  Create()
    {
        ItemDatabase asset = ScriptableObject.CreateInstance<ItemDatabase>();

        AssetDatabase.CreateAsset(asset, "Assets/ItemDatabase.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}