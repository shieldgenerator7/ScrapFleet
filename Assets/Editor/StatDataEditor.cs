using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatData))]
[CanEditMultipleObjects]
public class StatDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Description List"))
        {
            foreach (Object oTarget in targets)
            {
                ((StatData)oTarget).generateDescriptionList();
            }
        }
    }
}
