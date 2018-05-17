using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : MonoBehaviour
{
    private GameObject objPlayPack;
    private GameObject objPlayer;

    public EnemySpaceControl[] arrEnemySpace;
    public int nBossNo;

    public Transform PlayerStartPos;
    public Transform PlayerDestPos;

    public BoxCollider colBossGate;

    public GameObject objWinterMap;
    public GameObject objSummerMap;

    private void Awake()
    {
        SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.DUNGEON);

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

        objPlayPack = GameObject.FindGameObjectWithTag(Util.Tag.PLAY_PACK);
        int cnt = objPlayPack.transform.childCount;
        for (int i = 0; i < cnt; ++i)
        {
            objPlayPack.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        objPlayer = objPlayPack.transform.Find(Util.Tag.PLAYER).gameObject;
        objPlayer.GetComponent<PlayerControl>().SetDestPos(PlayerDestPos.position); // 목적지 설정
        SetPlayerPos();

        SetEnemyPos();
    }

    private void OnDestroy()
    {
        EnemyPool.Instace.DisableAll();
        GameManager.Instace.GameStart = false;
        int cnt = objPlayPack.transform.childCount;
        for (int i = 0; i < cnt; ++i)
        {
            objPlayPack.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 보스가 죽었으면
        if (arrEnemySpace[nBossNo].IsDie())
        {
            // 승리 팝업 띄우기
            if (!GameManager.Instace.NoMove)
            {
                SoundManager.Instance.StopBgm();

                PlayerManager.Instace.ClearDungeon(DungeonDBManager.Instace.DungeonNo);
                Instantiate(Resources.Load(Util.ResourcePath.POPUP_WIN));
                PlayerManager.Instace.SetWin();
            }
        }
        // 플레이어가 죽었으면!!
        if (!objPlayer.activeSelf)
        {
            // 패배 팝업 띄우기
            if (!GameManager.Instace.NoMove)
            {
                SoundManager.Instance.StopBgm();

                Instantiate(Resources.Load(Util.ResourcePath.POPUP_LOSE));
            }
        }

        // 게임이 끝났으면 (팝업 확인 눌렀을 때)
        if (!GameManager.Instace.GameStart)
        {
            SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.TOWN);
        }
    }

    private void SetPlayerPos()
    {
        // 네브매쉬 때문에 정확한 이동이 되지 않아서..
        objPlayer.SetActive(false);
        objPlayer.transform.SetPositionAndRotation(PlayerStartPos.position, PlayerStartPos.rotation);
        objPlayer.SetActive(true);
    }

    private void SetEnemyPos()
    {
        for (int i = 0; i < arrEnemySpace.Length; ++i)
        {
            arrEnemySpace[i].eEnemyNo = DungeonDBManager.Instace.GetCharacterType(i);
        }
    }

    // 보스 영역 입구에 도달 했을 때
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어라면
        if (other.gameObject.CompareTag(Util.Tag.PLAYER))
        {
            SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.BOSS);
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.ENEMY_BOSS);

            colBossGate.enabled = false;
            GameManager.Instace.TriggerBossGate(PlayerDestPos);
            GameObject objBossName = Instantiate(Resources.Load(Util.ResourcePath.UI_BOSS_NAME)) as GameObject;
            objBossName.GetComponent<BossName>().SetName(DungeonDBManager.Instace.GetEnemyName(nBossNo));
        }
    }
}
