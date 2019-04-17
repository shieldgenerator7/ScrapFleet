using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardGenerator))]
public class CardGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CardGenerator cg = (CardGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            cg.generate();
        }
        if (GUILayout.Button("Clear"))
        {
            cg.clearStats(true);
        }
    }
}
