using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardLayout : MonoBehaviour
{
    public string layoutName;
    public bool showsStats = true;//whether or not it shows stats
    [Header("Canvas Elements")]
    public Canvas canvas;
    public Image imgPortrait;
    public Text txtName;
    public TextMeshProUGUI txtTags;
    public TextMeshProUGUI txtEffect;
    public Image imgNameplate;
    
    public void toggleEnabled(bool enable)
    {
        enabled = enable;
        canvas.gameObject.SetActive(enable);
    }
}
