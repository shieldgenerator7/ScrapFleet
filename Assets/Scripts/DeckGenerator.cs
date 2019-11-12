using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DeckGenerator : MonoBehaviour
{
    public bool forceOneOfEach = false;//true to force only one of each card to be generated

    private IEnumerator generationProcess;

    /// <summary>
    /// The string data of what to write in the cards
    /// [number of cards]:[pilot?][accuracy][fire rate][speed][shields][hull]
    /// </summary>
    public List<DeckData> deckData;//25:t11115

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
    
    [MenuItem("SG7/Generate Deck %g")]
    public static void generateDeck()
    {
        FindObjectOfType<DeckGenerator>().generate();
    }

    [ContextMenu("GENERATE CONTEXT MENU")]
    public void generate()
    {
        if (generationProcess == null)
        {
            generationProcess = generateProcess();
            EditorApplication.update += generate;
        }
        else
        {
            generationProcess.MoveNext();
        }
    }
    IEnumerator generateProcess()
    {
        int fileSum = 0;
        int cardCountSum = 0;
        int estimatedCount = 500;//2019-04-17: TODO: actually calculate this estimate
        for (int i = 0; i < deckData.Count; i++)
        {
            //Get folder
            string folderName = deckData[i].name.Trim().Replace(" ", "_") + "/";
            if (!Directory.Exists(CardFolder + folderName))
            {
                Directory.CreateDirectory(CardFolder + folderName);
            }
            //Process cards of the deck
            List<CardData> cardData = deckData[i].cards;
            for (int j = 0; j < cardData.Count; j++)
            {
                CardData data = cardData[j];
                //Progress bar
                EditorUtility.DisplayProgressBar(
                    "Deck Generation",
                    "Generating " + deckData[i].name + "/ " + data.name,
                    (float)cardCountSum / (float)estimatedCount
                    );
                yield return null;
                int count = data.count;
                cardCountSum += count;
                if (forceOneOfEach)
                {
                    count = 1;
                }
                fileSum += count;
                CardGen.generate(data);
                Texture2D tex2d = CardGen.generateCardImage();
                byte[] bytes = tex2d.EncodeToPNG();
                string baseFileName = folderName + data.name.Trim().Replace(" ", "_");
                string fileExtension = ".png";
                if (forceOneOfEach)
                {
                    string filename = baseFileName + "[" + data.count + "]" + fileExtension;
                    SaveTextureToFile(bytes, filename);
                }
                else
                {
                    baseFileName += "_";
                    for (int c = 1; c <= count; c++)
                    {
                        string filename = baseFileName + c + fileExtension;
                        SaveTextureToFile(bytes, filename);
                    }
                }
            }
            //Make card back
            if (deckData[i].cardBack != null)
            {
                GameObject cardBack = Instantiate(CardGen.cardBack);
                SpriteRenderer cbSR = cardBack.GetComponent<SpriteRenderer>();
                cbSR.sortingOrder = 100;
                cbSR.sprite = deckData[i].cardBack;
                cbSR.color = Color.white;

                Texture2D tex2d = CardGen.generateCardImage();
                byte[] bytes = tex2d.EncodeToPNG();
                string filename = folderName + "back.png";
                SaveTextureToFile(bytes, filename);

                DestroyImmediate(cardBack);
            }
        }
        openCardFolder();
        CardGen.clearStats(true);
        Debug.Log("" + fileSum + " card files generated! Card count: " + cardCountSum + ". Time: " + System.DateTime.Now);

        EditorUtility.ClearProgressBar();
        generationProcess = null;
        EditorApplication.update -= generate;
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
        string folderPath = CardFolder + deckData[0].name.Replace(" ","_");
        Debug.Log("Opening folder: " + folderPath);
        EditorUtility.RevealInFinder(folderPath);
    }

    public string CardFolder
    {
        get
        {
            return Application.persistentDataPath + "/";
        }
    }
}
