using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckData))]
public class DeckDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DeckData deck = (DeckData)target;

        if (GUILayout.Button("Propogate Card Layout to Cards"))
        {
            foreach(CardData card in deck.cards)
            {
                card.cardLayoutName = deck.cardLayoutName;
            }
        }
    }
}
