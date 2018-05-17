using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : MonoBehaviour
{
    public GameObject objWinterMap;
    public GameObject objSummerMap;

    public Transform tfFromRobby;
    public Transform tfToRobby;
    public Transform tfFromDungeon;
    public Transform tfToDungeon;
    public Transform tfShop;
    public Transform tfFromShop;
    public Transform tfReinforce;
    public Transform tfFromReinforce;
    public Transform tfStory;
    public Transform tfFromStory;

    private GameObject objPlayer = null;
    private GameObject objPlayPack = null;

    private void Awake()
    {
        SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.TOWN);

        if (SceneCtrlManager.Instace.PrevSceneNo == E_SCENE_NO.ROBBY)
        {
            objPlayPack = Instantiate(Resources.Load(Util.ResourcePath.PLAY_PACK)) as GameObject;
            DontDestroyOnLoad(objPlayPack);
        }
        else
        {
            objPlayPack = GameObject.FindGameObjectWithTag(Util.Tag.PLAY_PACK);
            int cnt = objPlayPack.transform.childCount;
            for (int i = 0; i < cnt; ++i)
            {
                objPlayPack.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        objPlayer = objPlayPack.transform.Find(Util.Tag.PLAYER).gameObject;

        SetPlayerPos(SceneCtrlManager.Instace.PrevSceneNo);
    }

    // 플레이어 로드가 다 끝난 뒤 설정!!
    private void Start()
    {
        // == 현재 맵 종류 선택 ==
        // 스토리 진행이 다 끝났다면
        if (PlayerManager.Instace.CurrStoryNo >= StoryDBManager.Instace.GetMaxStoryNo())
        {
            Destroy(objWinterMap);
        }
        else
        {
            Destroy(objSummerMap);
        }
    }

    private void OnDestroy()
    {
        if (SceneCtrlManager.Instace.NextSceneNo != E_SCENE_NO.DUNGEON)
            Destroy(objPlayPack);

        SoundManager.Instance.StopBgm();
    }

    private void SetPlayerPos(E_SCENE_NO eType)
    {
        // 네브매쉬 때문에 정확한 이동이 되지 않아서..
        objPlayer.SetActive(false);

        switch (eType)
        {
            case E_SCENE_NO.ROBBY:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromRobby.position, tfFromRobby.rotation);
                }
                break;
            case E_SCENE_NO.DUNGEON:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromDungeon.position, tfFromDungeon.rotation);
                }
                break;
        }

        objPlayer.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((tfToRobby.position - objPlayer.transform.position).magnitude < 2.0f)
        {
            SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.ROBBY);
        }
        else if ((tfToDungeon.position - objPlayer.transform.position).magnitude < 5.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;

            // 퀘스트 성공
            if (PlayerManager.Instace.CurrQuestNo == (int)E_QUEST_LIST.FIND_DUNGEON)
            {
                GameManager.Instace.QuestChange = true;
                PlayerManager.Instace.ClearQuest = true;
            }

            SetPlayerPos(E_SCENE_NO.DUNGEON);
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_DUNGEON));
        }
        else if ((tfShop.position - objPlayer.transform.position).magnitude < 2.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;

            objPlayer.transform.SetPositionAndRotation(tfFromShop.position, tfFromShop.rotation);
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_SHOP));
        }
        else if ((tfReinforce.position - objPlayer.transform.position).magnitude < 2.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;

            objPlayer.transform.SetPositionAndRotation(tfFromReinforce.position, tfFromReinforce.rotation);
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_REINFORCE));
        }
        else if ((tfStory.position - objPlayer.transform.position).magnitude < 2.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;

            objPlayer.transform.SetPositionAndRotation(tfFromStory.position, tfFromStory.rotation);
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_STORY));
        }
    }

    private void Update()
    {
        // 게임을 시작 해야 하면
        if (GameManager.Instace.GameStart)
        {
            int cnt = objPlayPack.transform.childCount;
            for (int i = 0; i < cnt; ++i)
            {
                objPlayPack.transform.GetChild(i).gameObject.SetActive(false);
            }
            SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.DUNGEON);
        }
    }

    public void SetSummerMap()
    {
        if (objWinterMap)
        {
            Destroy(objWinterMap);
            objSummerMap = Instantiate(Resources.Load(Util.ResourcePath.MAP_TOWN_SUMMER)) as GameObject;
        }
    }
}
