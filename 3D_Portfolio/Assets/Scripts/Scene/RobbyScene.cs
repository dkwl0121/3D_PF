using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyScene : MonoBehaviour
{
    public void Awake()
    {
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
        SceneCtrlManager.Instace.SetNextScene(E_SCENE_NO.TOWN);
        SceneCtrlManager.Instace.ChangeScene();
    }
}
