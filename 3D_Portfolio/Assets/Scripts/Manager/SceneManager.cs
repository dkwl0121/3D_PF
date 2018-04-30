using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    private static SceneManager sInstance = null;
    public static SceneManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new SceneManager();
            }
            return sInstance;
        }
    }

    private E_SCENE_NO prevSceneNo;
    public int PrevSceneNo
    {
        get
        {
            return (int)prevSceneNo;
        }
    }
    private E_SCENE_NO currSceneNo;
    public int CurrSceneNo
    {
        get
        {
            return (int)currSceneNo;
        }
    }
    private E_SCENE_NO nextSceneNo;

    public void Setup()
    {
        prevSceneNo = E_SCENE_NO.ROBBY;
        currSceneNo = E_SCENE_NO.TOWN;
        nextSceneNo = E_SCENE_NO.TOWN;
    }

    public void SetNextScene(int eSceneNo)
    {
        nextSceneNo = (E_SCENE_NO)eSceneNo;
    }

    public void ChangeScene()
    {
        EnemyPool.Instace.DisableAll();
    }
}
