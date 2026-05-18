using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;
using System;

public class ScreenshotsMaker : MonoBehaviour
{
    public static ScreenshotsMaker instance;

    private  Camera mcamera;
    private int captureCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mcamera = Camera.main;
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24);
            //mcamera.targetTexture = rt;
            //Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
            //mcamera.Render();
            //RenderTexture.active = rt;
            //screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
            //mcamera.targetTexture = null;
            //RenderTexture.active = null;
            //Destroy(rt);
            //byte[] bytes = screenShot.EncodeToPNG();
            //string filename = ScreenShotName(captureWidth, captureHeight);
            //File.WriteAllBytes(filename, bytes);
        }
    }


    public void Make(int captureWidth, int captureHeight, string mFileName)
    {
        captureCount = PlayerPrefs.GetInt(mFileName + "capturecount");
        RenderTexture rt = new RenderTexture(captureWidth, captureHeight, 24);
        mcamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        mcamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, captureWidth, captureHeight), 0, 0);
        mcamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(captureWidth, captureHeight, mFileName+"_"+captureCount);
        File.WriteAllBytes(filename, bytes);
        captureCount += 1;
        PlayerPrefs.SetInt(mFileName+"capturecount", captureCount);
    }

    public static string ScreenShotName(int width, int height, string mFileName)
    {
        return string.Format(mFileName + ".png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }


}
