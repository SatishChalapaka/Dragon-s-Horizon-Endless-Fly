using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSCounter : MonoBehaviour
{
    //public Text fpsText;
    //private int lastFrameIndex;
    //private float[] frameDeltaTimeArray;
    //private void Awake()
    //{
    //    frameDeltaTimeArray = new float[60];
    //}
    //private void Update()
    //{
    //    frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
    //    lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
    //    fpsText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    //}
    //float CalculateFPS()
    //{
    //    float total = 0f;
    //    foreach (float deltaTime in frameDeltaTimeArray)
    //    {
    //        total += deltaTime;
    //    }
    //    return frameDeltaTimeArray.Length / total;
    //}
    //Assign this script to any object in the Scene to display frames per second 

    //public float updateInterval = 0.5f; //How often should the number update

    //float accum = 0.0f;

    //int frames = 0;

    //float timeleft;

    //float fps;

    //GUIStyle textStyle = new GUIStyle();

    //// Use this for initialization

    //void Start()

    //{

    //    timeleft = updateInterval;



    //    textStyle.fontStyle = FontStyle.Bold;

    //    textStyle.normal.textColor = Color.white;

    //}



    //// Update is called once per frame

    //void Update()

    //{

    //    timeleft -= Time.deltaTime;

    //    accum += Time.timeScale / Time.deltaTime;

    //    ++frames;

    //    // Interval ended - update GUI text and start new interval

    //    if (timeleft <= 0.0)

    //    {

    //        // display two fractional digits (f2 format)

    //        fps = (accum / frames);

    //        timeleft = updateInterval;

    //        accum = 0.0f;

    //        frames = 0;

    //    }

    //}



    //void OnGUI()

    //{

    //    //Display the fps and round to 2 decimals

    //    GUI.Label(new Rect(5, 5, 100, 25), fps.ToString("F2") + "FPS", textStyle);

    //}
    public Text fpsText;

    public float deltaTime;



    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        float fps = 1.0f / deltaTime;

        fpsText.text = Mathf.Ceil(fps).ToString();

    }
}
