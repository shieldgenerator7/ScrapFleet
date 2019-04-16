using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 2)]
public class CardData : ScriptableObject
{
    public new string name;
    public Sprite portrait;
    public List<string> tags;
    [TextArea(0,5)]
    public string effect;
    public int count;

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
            string result="";
            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    result += tag + ", ";
                }
                //Remove ending comma
                result = result.Substring(0, result.Length - 2);
                //Add ending period
                result += ".";
            }
            return result;
        }
    }
}
