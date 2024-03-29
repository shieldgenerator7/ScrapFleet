﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardGenerator : MonoBehaviour
{
    [Header("Stats")]
    public bool pilot;
    public bool autopilot;
    [Range(0, 5)]
    public int aim;
    [Range(0, 5)]
    public int shots;
    [Range(0, 5)]
    public int speed;
    [Range(0, 5)]
    public int shields;
    [Range(0, 5)]
    public int hull;

    public CardData cardData;

    [Header("Stats")]
    public List<StatData> stats;

    [Header("Generation")]
    public float safeEdge = 0.25f;
    public float decrement = 0.23f;
    public float startY = 3.5f - 0.25f;
    public Vector2 cardSize;
    public Vector2 resolution = new Vector2(825, 1125);
    public GameObject cardBack;

    [Header("Prefabs")]
    public GameObject PilotPrefab;
    public GameObject AutoPilotPrefab;
    public GameObject AimPrefab;
    public GameObject ShotsPrefab;
    public GameObject SpeedPrefab;
    public GameObject ShieldsPrefab;
    public GameObject HullPrefab;

    [Header("Card Layouts")]
    public SpriteRenderer imgBorder;
    public List<CardLayout> layouts;
    private int layoutIndex = 0;
    public string LayoutName
    {
        get => layouts[layoutIndex].layoutName;
        set
        {
            for (int i = 0; i < layouts.Count; i++)
            {
                CardLayout layout = layouts[i];
                if (layout.layoutName == value)
                {
                    layout.toggleEnabled(true);
                    layoutIndex = i;
                }
                else
                {
                    layout.toggleEnabled(false);
                }
            }
        }
    }
    public CardLayout Layout
    {
        get => layouts[layoutIndex];
        set
        {
            for (int i = 0; i < layouts.Count; i++)
            {
                CardLayout layout = layouts[i];
                if (layout == value)
                {
                    layout.toggleEnabled(true);
                    layoutIndex = i;
                }
                else
                {
                    layout.toggleEnabled(false);
                }
            }
        }
    }

    [Header("Guides")]
    public GameObject guide;

    private void setStats(CardData data)
    {
        cardData = data;
        pilot = data.pilot;
        autopilot = data.autopilot;
        aim = data.aim;
        shots = data.shots;
        speed = data.speed;
        shields = data.shields;
        hull = data.hull;
    }

    [MenuItem("SG7/Generate Card %#g")]
    public static void generateCard()
    {
        FindObjectOfType<CardGenerator>().generate();
        //2019-11-12: copied from https://forum.unity.com/threads/solved-how-to-force-update-in-edit-mode.561436/#post-5110952
        EditorApplication.QueuePlayerLoopUpdate();
        SceneView.RepaintAll();
    }

    public void generate(CardData stats = null)
    {
        //Clear the stats
        clearStats(true);
        //Hide the guide
        guide.SetActive(false);
        //Set the stats
        if (stats == null)
        {
            stats = cardData;
        }
        setStats(stats);
        //Set card layout
        LayoutName = stats.cardLayoutName;
        //Center the camera
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = cardSize.x / 2;
        camPos.y = cardSize.y / 2;
        Camera.main.transform.position = camPos;
        //Generate the card stats
        if (Layout.showsStats)
        {
            float currentY = startY;//where the next stat is going to be placed
                                    //Pilot
            if (pilot)
            {
                GameObject stat = Instantiate(PilotPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
                //Show the nameplate for pilots only
                Layout.imgNameplate.enabled = true;
            }
            else
            {
                Layout.imgNameplate.enabled = false;
            }
            //Auto Pilot
            if (autopilot)
            {
                GameObject stat = Instantiate(AutoPilotPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }
            //Aim
            for (int i = 0; i < aim; i++)
            {
                GameObject stat = Instantiate(AimPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }
            //Shots
            for (int i = 0; i < shots; i++)
            {
                GameObject stat = Instantiate(ShotsPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }
            //Speed
            for (int i = 0; i < speed; i++)
            {
                GameObject stat = Instantiate(SpeedPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }
            //Shield
            for (int i = 0; i < shields; i++)
            {
                GameObject stat = Instantiate(ShieldsPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }
            //Hull
            for (int i = 0; i < hull; i++)
            {
                GameObject stat = Instantiate(HullPrefab);
                Vector3 pos = stat.transform.position;
                pos.y = currentY;
                stat.transform.position = pos;
                currentY -= decrement;
            }

            //Do the same for the other side
            copyFlipped(cardSize / 2);
        }

        //Center the canvas
        Vector3 canvasPos = Layout.canvas.transform.position;
        canvasPos.x = cardSize.x / 2;
        canvasPos.y = cardSize.y / 2;
        Layout.canvas.transform.position = canvasPos;
        if (cardData)
        {
            //Set the canvas element data
            //Portrait
            if (Layout.imgPortrait)
            {
                Layout.imgPortrait.enabled = cardData.portrait;
                Layout.imgPortrait.sprite = cardData.portrait;
            }
            //Card Name
            if (Layout.txtName)
            {
                Layout.txtName.enabled = cardData.name != null && cardData.name != "";
                Layout.txtName.text = cardData.name;
            }
            //Tags
            if (Layout.txtTags)
            {
                string tagString = cardData.TagString;
                Layout.txtTags.enabled = tagString != null && tagString != "";
                Layout.txtTags.text = cardData.TagString;
            }
            //Effect
            if (Layout.txtEffect)
            {
                Layout.txtEffect.enabled =
                    (cardData.effect != null && cardData.effect != "")
                    || cardData.autoGenerateStatDescriptionLevel > 0;
                Layout.txtEffect.text = cardData.effect;

                //Stat Description
                if (cardData.autoGenerateStatDescriptionLevel > 0)
                {
                    bool newLine = Layout.txtEffect.text.Trim().Length > 0;
                    foreach (StatData stat in this.stats)
                    {
                        if (stat.getStat(cardData) > 0)
                        {
                            Layout.txtEffect.text += ((newLine) ? "\n" : "")
                                + "<sprite=" + stat.iconIndex + ">"
                                + " <color=#" + stat.colorHex + "><b>" + stat.name + "</b></color>:";
                            for (int i = 0;
                                i < cardData.autoGenerateStatDescriptionLevel
                                && i < stat.descriptionList.Count;
                                i++
                                )
                            {
                                Layout.txtEffect.text += " " + stat.descriptionList[i];
                            }
                            newLine = true;
                        }
                    }
                }
            }
            //Border
            imgBorder.enabled = cardData.border != null;
            if (cardData.border)
            {
                imgBorder.sprite = cardData.border;
            }
        }
    }

    public void clearStats(bool resetStats = false)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stat"))
        {
            DestroyImmediate(go);
        }

        if (resetStats)
        {
            pilot = false;
            autopilot = false;
            aim = 0;
            shots = 0;
            speed = 0;
            shields = 0;
            hull = 0;
        }
    }

    void copyFlipped(Vector2 center)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stat"))
        {
            GameObject stat = Instantiate(go);
            //stat.transform.localEulerAngles = new Vector3(0, 0, 180);
            stat.transform.RotateAround(center, new Vector3(0, 0, 1), 180);
            //Vector3 pos = stat.transform.position;
            //pos.y = currentY;
            //stat.transform.position = pos;
            //currentY -= decrement;
        }
    }

    /// <summary>
    /// Grabs image data from card
    /// 2019-04-15: copied from Stonicorn.CheckPointChecker.grabCheckPointCameraData()
    /// </summary>
    /// <returns></returns>
    public Texture2D generateCardImage()
    {//2016-12-06: The following code copied from an answer by jashan: http://answers.unity3d.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html
        int resWidth = (int)resolution.x;
        int resHeight = (int)resolution.y;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        screenShot.Apply();
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        return screenShot;
    }
}
