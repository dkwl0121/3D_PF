using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : MonoBehaviour
{
    public Transform tfFromRobby;
    public Transform tfToRobby;
    public Transform tfFromDungeon;
    public Transform tfToDungeon;
    public Transform tfShop;
    
    private GameObject objPlayer = null;
    private GameObject objPlayPack = null;

    private void Awake()
    {
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

    private void OnDestroy()
    {
        if (SceneCtrlManager.Instace.NextSceneNo != E_SCENE_NO.DUNGEON)
            Destroy(objPlayPack);
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
        if ((tfToRobby.position - objPlayer.transform.position).magnitude < 5.0f)
        {
            SceneCtrlManager.Instace.ChangeScene(E_SCENE_NO.ROBBY);
        }
        else if ((tfToDungeon.position - objPlayer.transform.position).magnitude < 5.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;

            SetPlayerPos(E_SCENE_NO.DUNGEON);
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_DUNGEON));
        }
        else if ((tfShop.position - objPlayer.transform.position).magnitude < 5.0f)
        {
            // 이미 팝업이 열려 있다면
            if (GameManager.Instace.NoMove) return;
            
            Instantiate(Resources.Load(Util.ResourcePath.POPUP_SHOP));
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
}
