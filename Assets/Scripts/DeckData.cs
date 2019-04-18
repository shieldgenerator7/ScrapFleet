using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckData", menuName = "DeckData", order = 3)]
public class DeckData : ScriptableObject
{
    public new string name;
    public Sprite cardBack;
    public List<CardData> cards;
}
