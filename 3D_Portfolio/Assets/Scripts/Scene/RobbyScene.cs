using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyScene : MonoBehaviour
{
    public void Awake()
    {
        // 셋업을 한번만 함.
        if (GameManager.Instace.Created) return;
        GameManager.Instace.Setup();
        LevelDBManager.Instace.Setup();
        EnemyDBManager.Instace.Setup();
        DungeonDBManager.Instace.Setup();
        FrameManager.Instace.Setup();
        PlayerManager.Instace.Setup();
        EnemyPool.Instace.Setup();
        DamageValuePool.Instace.Setup();
    }

    public void NewGame()
    {
        PlayerManager.Instace.NewGame = true;
        LoadTownScene();
    }
    
    public void MyGame()
    {
        PlayerManager.Instace.NewGame = false;
        LoadTownScene();
    }

    public void Exit()
    {
        CommonFunction.ExitGame();
    }

    private void LoadTownScene()
    {
        SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.TOWN);
    }
}
