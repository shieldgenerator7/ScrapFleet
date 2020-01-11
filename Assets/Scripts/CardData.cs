using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 2)]
public class CardData : ScriptableObject
{
    public new string name;
    public Sprite portrait;
    public Sprite border;
    public List<string> tags;
    [TextArea(0, 5)]
    public string effect;
    public int count;
    public bool showText = true;
    public int autoGenerateStatDescriptionLevel = 1;
    public string cardLayoutName;

    [Header("Stats")]
    public bool pilot;
    public bool autopilot;
    [Range(0, 5)]
    public int accuracy;
    [Range(0, 5)]
    public int fireRate;
    [Range(0, 5)]
    public int speed;
    [Range(0, 5)]
    public int shields;
    [Range(0, 5)]
    public int hull;

    public string TagString
    {
        get
        {
            string result = "";
            if (pilot)
            {
                result += "<sprite=5> PILOT, ";
            }
            if (autopilot)
            {
                result += "<sprite=6> AUTOPILOT, ";
            }
            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    result += tag + ", ";
                }
            }
            if (result.Length > 0)
            {
                //Remove ending comma
                result = result.Substring(0, result.Length - 2);
                //Add ending period
                //result += ".";
            }
            return result;
        }
    }
}
