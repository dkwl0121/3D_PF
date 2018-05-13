using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private static FrameManager sInstance = null;
    public static FrameManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gObject = new GameObject("_FrameManager");
                sInstance = gObject.AddComponent<FrameManager>();
            }
            return sInstance;
        }
    }

    float deltaTime = 0.0f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Setup()
    {

    }

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
