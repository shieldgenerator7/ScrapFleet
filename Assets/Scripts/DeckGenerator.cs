using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    /// <summary>
    /// The string data of what to write in the cards
    /// [number of cards]:[pilot?][accuracy][fire rate][speed][shields][hull]
    /// </summary>
    public List<string> cardData;//25:t11115

    private CardGenerator cardGenerator;
    public CardGenerator CardGen
    {
        get
        {
            if (cardGenerator == null)
            {
                cardGenerator = FindObjectOfType<CardGenerator>();
            }
            return cardGenerator;
        }
    }

    public void generate()
    {
        string[] data = cardData[0].Split(':');
        int count = int.Parse(data[0]);
        CardGen.setStats(data[1]);
        CardGen.generate();
    }
}
