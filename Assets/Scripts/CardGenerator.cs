using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [Header("Stats")]
    public bool pilot;
    public int accuracy;
    public int fireRate;
    public int speed;
    public int shields;
    public int hull;

    [Header("Generation")]
    public float safeEdge = 0.25f;
    public float decrement = 0.23f;
    public float startY = 3.5f - 0.25f;

    [Header("Prefabs")]
    public GameObject PilotPrefab;
    public GameObject AccuracyPrefab;
    public GameObject FireRatePrefab;
    public GameObject SpeedPrefab;
    public GameObject ShieldsPrefab;
    public GameObject HullPrefab;


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
    }

    void clearStats()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stat"))
        {
            DestroyImmediate(go);
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
}
