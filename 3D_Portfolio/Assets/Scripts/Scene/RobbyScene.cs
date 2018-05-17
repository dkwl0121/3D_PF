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
        WeaponDBManager.Instace.Setup();
        ItemDBManager.Instace.Setup();
        QuestDBManager.Instace.Setup();
        StoryDBManager.Instace.Setup();
        FrameManager.Instace.Setup();
        PlayerManager.Instace.Setup();
        SoundManager.Instance.Setup();
        EnemyPool.Instace.Setup();
        DamageValuePool.Instace.Setup();

        SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.ROBBY);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.StopBgm();
    }

    public void NewGame()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        PlayerManager.Instace.NewGame = true;
        LoadTownScene();
    }
    
    public void MyGame()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        PlayerManager.Instace.NewGame = false;
        LoadTownScene();
    }

    public void Exit()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        Instantiate(Resources.Load(Util.ResourcePath.POPUP_EXIT));
    }

    private void LoadTownScene()
    {
        SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.TOWN);
    }
}
