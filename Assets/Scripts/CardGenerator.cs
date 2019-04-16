using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
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

    public CardData cardData;

    [Header("Generation")]
    public float safeEdge = 0.25f;
    public float decrement = 0.23f;
    public float startY = 3.5f - 0.25f;

    [Header("Prefabs")]
    public GameObject PilotPrefab;
    public GameObject AutoPilotPrefab;
    public GameObject AccuracyPrefab;
    public GameObject FireRatePrefab;
    public GameObject SpeedPrefab;
    public GameObject ShieldsPrefab;
    public GameObject HullPrefab;

    [Header("Canvas Elements")]
    public Image imgPortrait;
    public Text txtName;
    public Text txtTags;
    public Text txtEffect;

    public void setStats(CardData data)
    {
        cardData = data;
        pilot = data.pilot;
        autopilot = data.autopilot;
        accuracy = data.accuracy;
        fireRate = data.fireRate;
        speed = data.speed;
        shields = data.shields;
        hull = data.hull;
    }

    public void generate()
    {
        clearStats();
        float currentY = startY;//where the next stat is going to be placed
        //Pilot
        if (pilot)
        {
            GameObject stat = Instantiate(PilotPrefab);
            Vector3 pos = stat.transform.position;
            pos.y = currentY;
            stat.transform.position = pos;
            currentY -= decrement;
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
        //Accuracy
        for (int i = 0; i < accuracy; i++)
        {
            GameObject stat = Instantiate(AccuracyPrefab);
            Vector3 pos = stat.transform.position;
            pos.y = currentY;
            stat.transform.position = pos;
            currentY -= decrement;
        }
        //Fire Rate
        for (int i = 0; i < fireRate; i++)
        {
            GameObject stat = Instantiate(FireRatePrefab);
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
        copyFlipped(new Vector2(2.5f / 2, 3.5f / 2));

        if (cardData)
        {
            //Set the canvas element data
            //Portrait
            imgPortrait.sprite = cardData.portrait;
            imgPortrait.enabled = imgPortrait.sprite != null;
            //Name
            txtName.text = cardData.name;
            //Tags
            txtTags.text = cardData.TagString;
            //Effect
            txtEffect.text = cardData.effect;
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
            accuracy = 0;
            fireRate = 0;
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
        int resWidth = 650;
        int resHeight = 909;
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
