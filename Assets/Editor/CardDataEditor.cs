using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CardData cardData = (CardData)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate Card"))
        {
            CardGenerator cg = FindObjectOfType<CardGenerator>();
            cg.cardData = cardData;
            cg.generate();
            Selection.activeGameObject = cg.gameObject;
        }
    }
}
