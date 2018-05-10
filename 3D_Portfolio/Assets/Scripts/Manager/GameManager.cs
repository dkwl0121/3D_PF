using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager sInstance = null;
    public static GameManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameManager();
            }
            return sInstance;
        }
    }

    private bool isCreated = false;
    public bool Created { get { return isCreated; } }

    private bool isPopup = false;
    public bool Popup { get { return isPopup; } set { isPopup = value; } }

    private bool isGameStart = false;
    public bool GameStart { get { return isGameStart; } set { isGameStart = value; } }

    private bool isPerformance = false;
    public bool Performance { get { return isPerformance; } set { isPerformance = value; } }

    public void Setup()
    {
        isCreated = true;
    }
    
    // 보스 영역 입구에 도달 했을 때
    public void TriggerBossGate(Transform tfBoss)
    {
        isPerformance = true;
        Camera.main.GetComponent<CameraControl>().CameraCtrlForBoss(tfBoss);
    }
}
