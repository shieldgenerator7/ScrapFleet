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
        int sum = 0;
        for (int i = 0; i < cardData.Count; i++)
        {
            CardData data = cardData[i];
            int count = data.count;
            if (forceOneOfEach)
            {
                count = 1;
            }
            sum += count;
            CardGen.setStats(data);
            CardGen.generate();
            Texture2D tex2d = CardGen.generateCardImage();
            byte[] bytes = tex2d.EncodeToPNG();
            string baseFileName = data.name.Trim().Replace(" ", "_");
            string fileExtension = ".png";
            if (forceOneOfEach)
            {
                string filename = baseFileName + "[" + data.count + "]" + fileExtension;
                SaveTextureToFile(bytes, filename);
            }
            else
            {
                baseFileName += "_";
                for (int j = 1; j <= count; j++)
                {
                    string filename = baseFileName + j + fileExtension;
                    SaveTextureToFile(bytes, filename);
                }
            }
        }
        openCardFolder();
        CardGen.clearStats(true);
        Debug.Log("" + sum + " cards generated! Time: " + System.DateTime.Now);
    }

    /// <summary>
    /// Saves the given texture to the given filename
    /// 2019-04-15: copied from http://answers.unity.com/answers/245658/view.html
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    private void SaveTextureToFile(byte[] bytes, string fileName)
    {
        FileStream file = File.Open(CardFolder + fileName, FileMode.Create);
        BinaryWriter binary = new BinaryWriter(file);
        binary.Write(bytes);
        file.Close();
    }

    public void openCardFolder()
    {
        EditorUtility.RevealInFinder(CardFolder + Application.productName);
    }

    public string CardFolder
    {
        get
        {
            return Application.persistentDataPath + "/";
        }
    }
}
