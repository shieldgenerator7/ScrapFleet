using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckGenerator))]
public class DeckGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DeckGenerator cg = (DeckGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            cg.generate();
        }
    }
}
