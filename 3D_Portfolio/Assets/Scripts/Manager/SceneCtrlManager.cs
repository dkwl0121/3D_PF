using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrlManager
{
    private static SceneCtrlManager sInstance = null;
    public static SceneCtrlManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new SceneCtrlManager();
            }
            return sInstance;
        }
    }

    private E_SCENE_NO prevSceneNo;
    public E_SCENE_NO PrevSceneNo
    {
        get
        {
            return prevSceneNo;
        }
    }
    private E_SCENE_NO currSceneNo;
    public E_SCENE_NO CurrSceneNo
    {
        get
        {
            return currSceneNo;
        }
    }
    private E_SCENE_NO nextSceneNo;
    public E_SCENE_NO NextSceneNo
    {
        get
        {
            return nextSceneNo;
        }
    }

    public void Setup()
    {
        prevSceneNo = E_SCENE_NO.TITLE;
        currSceneNo = E_SCENE_NO.TITLE;
        nextSceneNo = E_SCENE_NO.TITLE;
    }

    public void SetNextScene(E_SCENE_NO eSceneNo)
    {
        nextSceneNo = eSceneNo;
    }

    public void ChangeScene()
    {
        if (currSceneNo == E_SCENE_NO.DUNGEON)
            EnemyPool.Instace.DisableAll();

        prevSceneNo = currSceneNo;
        currSceneNo = nextSceneNo;
        SceneManager.LoadScene((int)E_SCENE_NO.LOADING);
    }
}
