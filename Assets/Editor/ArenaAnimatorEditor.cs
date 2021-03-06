using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ArenaAnimator))]
public class ArenaAnimatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ArenaAnimator arenaAnimator = (ArenaAnimator) target;
        //EditorGUILayout.
        if(GUILayout.Button("Match Gradient Start To Material Color"))
        {
            arenaAnimator.MatchMaterialColor();
        }
        if (GUILayout.Button("Refresh"))
        {
            arenaAnimator.Start();
        }
    }
}
