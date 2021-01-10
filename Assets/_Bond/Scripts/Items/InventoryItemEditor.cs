using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//Eugene

public class ItemEditor : EditorWindow 
{

    public ItemDatabase ItemDatabase;
    private int viewIndex = 1;

    [MenuItem ("Window/Inventory Item Editor %#e")]
    static void  Init () 
    {
        EditorWindow.GetWindow (typeof (ItemEditor));
    }

    void  OnEnable () 
    {
        if(EditorPrefs.HasKey("ObjectPath")) 
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            ItemDatabase = AssetDatabase.LoadAssetAtPath (objectPath, typeof(ItemDatabase)) as ItemDatabase;
        }

    }

    void  OnGUI () 
    {
        GUILayout.BeginHorizontal ();
        GUILayout.Label ("Inventory Item Editor", EditorStyles.boldLabel);
        if (ItemDatabase != null) 
        {
            if (GUILayout.Button("Show Item List")) 
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = ItemDatabase;
            }
        }
        if (GUILayout.Button("Open Item List")) 
        {
                OpenItemList();
        }
        if (GUILayout.Button("New Item List")) 
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = ItemDatabase;
        }
        GUILayout.EndHorizontal ();

        if (ItemDatabase == null) 
        {
            GUILayout.BeginHorizontal ();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false))) 
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal ();
        }
        
        GUILayout.Space(20);

        if (ItemDatabase != null) 
        {
            GUILayout.BeginHorizontal ();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex > 1)
                    viewIndex --;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
            {
                if (viewIndex < ItemDatabase.itemList.Count) 
                {
                    viewIndex ++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal ();
            if (ItemDatabase.itemList == null)
            {
                Debug.Log("Inventory is empty");
            }
            if (ItemDatabase.itemList.Count > 0) 
            {
                GUILayout.BeginHorizontal ();
                viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, ItemDatabase.itemList.Count);
                //Mathf.Clamp (viewIndex, 1, ItemDatabase.itemList.Count);
                EditorGUILayout.LabelField ("of   " +  ItemDatabase.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                ItemDatabase.itemList[viewIndex-1].itemName = EditorGUILayout.TextField ("Item Name", ItemDatabase.itemList[viewIndex-1].itemName as string);
                ItemDatabase.itemList[viewIndex-1].prefab = (GameObject) EditorGUILayout.ObjectField("Prefab", ItemDatabase.itemList[viewIndex-1].prefab, typeof (GameObject), false);
                ItemDatabase.itemList[viewIndex-1].itemIcon = EditorGUILayout.ObjectField ("Item Icon", ItemDatabase.itemList[viewIndex-1].itemIcon, typeof (Texture2D), false) as Texture2D;
                ItemDatabase.itemList[viewIndex-1].itemObject = EditorGUILayout.ObjectField ("Item Object", ItemDatabase.itemList[viewIndex-1].itemObject, typeof (Rigidbody), false) as Rigidbody;

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                ItemDatabase.itemList[viewIndex-1].isUnique = (bool)EditorGUILayout.Toggle("Unique", ItemDatabase.itemList[viewIndex-1].isUnique, GUILayout.ExpandWidth(false));
                ItemDatabase.itemList[viewIndex-1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", ItemDatabase.itemList[viewIndex-1].isIndestructible,  GUILayout.ExpandWidth(false));
                ItemDatabase.itemList[viewIndex-1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", ItemDatabase.itemList[viewIndex-1].isQuestItem,  GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                ItemDatabase.itemList[viewIndex-1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", ItemDatabase.itemList[viewIndex-1].isStackable , GUILayout.ExpandWidth(false));
                ItemDatabase.itemList[viewIndex-1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", ItemDatabase.itemList[viewIndex-1].destroyOnUse,  GUILayout.ExpandWidth(false));
                ItemDatabase.itemList[viewIndex-1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", ItemDatabase.itemList[viewIndex-1].encumbranceValue,  GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                ItemDatabase.itemList[viewIndex-1].creatureFood = (bool)EditorGUILayout.Toggle("Food for Creature ", ItemDatabase.itemList[viewIndex-1].creatureFood , GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal ();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal ();
                ItemDatabase.itemList[viewIndex-1].spawnPoints = EditorGUILayout.Vector3Field("Spawn Position", ItemDatabase.itemList[viewIndex-1].spawnPoints , GUILayout.ExpandWidth(false));;
                GUILayout.EndHorizontal ();
            } 
            else 
            {
                GUILayout.Label ("This Inventory List is Empty.");
            }
        }
        if (GUI.changed) 
        {
            EditorUtility.SetDirty(ItemDatabase);
        }
    }

    void CreateNewItemList () 
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        ItemDatabase = CreateItemDatabase.Create();
        if (ItemDatabase) 
        {
            ItemDatabase.itemList = new List<Item>();
            string relPath = AssetDatabase.GetAssetPath(ItemDatabase);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList () 
    {
        string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath)) 
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            ItemDatabase = AssetDatabase.LoadAssetAtPath (relPath, typeof(ItemDatabase)) as ItemDatabase;
            if (ItemDatabase.itemList == null)
            {
                ItemDatabase.itemList = new List<Item>();
            }
            if (ItemDatabase) 
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem () 
    {
        Item newItem = new Item();
        newItem.itemName = "New Item";
        ItemDatabase.itemList.Add (newItem);
        viewIndex = ItemDatabase.itemList.Count;
    }

    void DeleteItem (int index) 
    {
        ItemDatabase.itemList.RemoveAt (index);
    }
}