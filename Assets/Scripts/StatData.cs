using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "StatData", order = 3)]
public class StatData : ScriptableObject
{
    public Stat stat;
    public new string name;//the name of the stat
    public Sprite symbol;//the graphic used to represent the stat on the card
    public int iconIndex;//the index of this stat's symbol in the stat sprite sheet (base 0)
    public Color color = Color.white;
    public string colorHex = "FFFFFFFF";
    [TextArea(3,5)]
    public string description;//the stat description

    public int getStat(CardData card)
    {
        switch (stat)
        {
            case Stat.ACCURACY:
                return card.accuracy;
            case Stat.FIRE_RATE:
                return card.fireRate;
            case Stat.SPEED:
                return card.speed;
            case Stat.SHIELDS:
                return card.shields;
            case Stat.HULL:
                return card.hull;
            default:
                throw new UnityException("StatData.stat not set to a countable stat! stat: " + stat);
        }
    }
}
