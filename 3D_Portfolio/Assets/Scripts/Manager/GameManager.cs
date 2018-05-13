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

    //private bool isPopup = false;
    //public bool Popup { get { return isPopup; } set { isPopup = value; } }

    private bool isGameStart = false;
    public bool GameStart { get { return isGameStart; } set { isGameStart = value; } }

    private bool isNoMove = false;
    public bool NoMove { get { return isNoMove; } set { isNoMove = value; } }

    public void Setup()
    {
        isCreated = true;
    }
    
    // 보스 영역 입구에 도달 했을 때
    public void TriggerBossGate(Transform tfBoss)
    {
        isNoMove = true;
        Camera.main.GetComponent<CameraControl>().CameraCtrlForBoss(tfBoss);
    }

    public void Exit(E_SCENE_NO eScene)
    {
        switch (eScene)
        {
            case E_SCENE_NO.ROBBY:
                CommonFunction.ExitGame();
                break;
            case E_SCENE_NO.TOWN:
                SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.ROBBY);
                break;
            case E_SCENE_NO.DUNGEON:
                SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.TOWN);
                break;
        }
    }
}
