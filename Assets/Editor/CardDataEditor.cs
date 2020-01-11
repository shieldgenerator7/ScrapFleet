using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData))]
[CanEditMultipleObjects]
public class CardDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (targets.Length == 1)
        {
            CardData cardData = (CardData)target;

            if (GUILayout.Button("Generate Card"))
            {
                CardGenerator cg = FindObjectOfType<CardGenerator>();
                cg.cardData = cardData;
                cg.generate();
                EditorApplication.QueuePlayerLoopUpdate();
                SceneView.RepaintAll();
            }
        }
    }
}
