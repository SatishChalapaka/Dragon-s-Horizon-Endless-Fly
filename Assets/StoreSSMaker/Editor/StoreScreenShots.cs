using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class StoreScreenShots : EditorWindow
{

    private bool Appstore;
    private bool Playstore;
    //private string destination;
    private enum Orientation
    {
        Potrait,
        Landscape
    }
 

    private Orientation orientation;

    [Serializable]
    public class IpadPro
    {
        public int width = 2048;
        public int height = 2732;
    }

    [Serializable]
    public class IphoneSmall
    {
        public int width = 1242;
        public int height = 2208;
    }

    [Serializable]
    public class IphoneMidium
    {
        public int width = 1242;
        public int height = 2688;
    }

    [Serializable]
    public class IphoneLarge
    {
        public int width = 1290;
        public int height = 2796;
    }

    [Serializable]
    public class AndroidResolution
    {
        public int width = 1080;
        public int height = 1920;
    }

    private IphoneLarge iphoneLarge;
    private IphoneMidium iphoneMidium;
    private IphoneSmall iphoneSmall;
    private IpadPro ipadPro;

    private AndroidResolution androidResolution;

    private void OnEnable()
    {
        
    }


    [MenuItem("StoreScreenShots/Make")]
    public static void OpenWindow()
    {
        GetWindow(typeof(StoreScreenShots));
    }


    int captureWidth;
    int captureHeight;
    private void OnGUI()
    {
        GUILayout.Label("Select Store", EditorStyles.boldLabel);
        GUILayout.Space(20);
        Appstore = EditorGUILayout.Toggle("Appstore", Appstore);
        GUILayout.Space(10);
        Playstore = EditorGUILayout.Toggle("Playstore", Playstore);
        GUILayout.Space(10);
        orientation =(Orientation) EditorGUILayout.EnumPopup("Orientation", orientation);
        //destination = EditorGUILayout.TextField("Destination", destination);

        GUILayout.Space(20);
        if (GUILayout.Button("Take SS"))
        {
            
            if (Appstore)
            {
                switch (orientation)
                {
                    case Orientation.Landscape:
                        //iphone large
                        ScreenshotsMaker.instance.Make(iphoneLarge.height, iphoneLarge.width,"iphonelarge_" + iphoneLarge.height +"x"+iphoneLarge.width);
                        //iphine medium
                        ScreenshotsMaker.instance.Make(iphoneMidium.height, iphoneMidium.width, "iphonemidium_" + iphoneMidium.height + "x" + iphoneMidium.width);
                        //iphone small
                        ScreenshotsMaker.instance.Make(iphoneSmall.height, iphoneSmall.width, "iphonesmall_" + iphoneSmall.height + "x" + iphoneSmall.width);
                        //ipad pro
                        ScreenshotsMaker.instance.Make(ipadPro.height, ipadPro.width, "ipadpro_" + ipadPro.height + "x" + ipadPro.width);
                        break;
                    case Orientation.Potrait:
                        //iphone large
                        ScreenshotsMaker.instance.Make(iphoneLarge.width, iphoneLarge.height, "iphonelarge_" + iphoneLarge.width + "x" + iphoneLarge.height);
                        //iphine medium
                        ScreenshotsMaker.instance.Make(iphoneMidium.width, iphoneMidium.height, "iphonemidium_" + iphoneMidium.width + "x" + iphoneMidium.height);
                        //iphone small
                        ScreenshotsMaker.instance.Make(iphoneSmall.width, iphoneSmall.height, "iphonesmall_" + iphoneSmall.width + "x" + iphoneSmall.height);
                        //ipad pro
                        ScreenshotsMaker.instance.Make(ipadPro.width, ipadPro.height, "ipadpro_" + ipadPro.width + "x" + ipadPro.height);
                        break;
                }
            }

            if (Playstore)
            {
                switch (orientation)
                {
                    case Orientation.Landscape:
                        //android
                        ScreenshotsMaker.instance.Make(androidResolution.height, androidResolution.width, "android_" + androidResolution.height + "x" + androidResolution.width);
                        break;
                    case Orientation.Potrait:
                        //android
                        ScreenshotsMaker.instance.Make(androidResolution.width, androidResolution.height, "android_" + androidResolution.width + "x" + androidResolution.height);
                        break;
                }
            }



        }

        GUILayout.Space(10);
        GUILayout.TextArea("Made by Raj", EditorStyles.centeredGreyMiniLabel);

    }


}
