using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DeckGenerator : MonoBehaviour
{
    public bool forceOneOfEach = false;//true to force only one of each card to be generated

    /// <summary>
    /// The string data of what to write in the cards
    /// [number of cards]:[pilot?][accuracy][fire rate][speed][shields][hull]
    /// </summary>
    public List<CardData> cardData;//25:t11115

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
        for (int i = 0; i < cardData.Count; i++)
        {
            CardData data = cardData[i];
            int count = data.count;
            if (forceOneOfEach)
            {
                count = 1;
            }
            CardGen.setStats(data);
            CardGen.generate();
            Texture2D tex2d = CardGen.generateCardImage();
            string baseFileName = data.name.Replace(" ", "_") + "_";
            for (int j = 1; j <= count; j++)
            {
                string filename = baseFileName + j + ".png";
                SaveTextureToFile(tex2d, filename);
            }
        }
        EditorUtility.RevealInFinder(Application.persistentDataPath + "/" + Application.productName);
    }

    /// <summary>
    /// Saves the given texture to the given filename
    /// 2019-04-15: copied from http://answers.unity.com/answers/245658/view.html
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    private void SaveTextureToFile(Texture2D texture, string fileName)
    {
        byte[] bytes = texture.EncodeToPNG();
        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Create);
        BinaryWriter binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }
}
