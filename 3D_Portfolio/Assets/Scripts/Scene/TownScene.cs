using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : MonoBehaviour
{
    public Transform tfFromRobby;
    //public Transform tfFromShop;
    //public Transform tfDungeon1;

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
        
        SetPlayerPos();

        SceneCtrlManager.Instace.SetNextScene(E_SCENE_NO.DUNGEON);
        SceneCtrlManager.Instace.ChangeScene();
    }

    private void OnDestroy()
    {
        // 로비씬으로 나가는 거면 저장하고 디스트로이
        int cnt = objPlayPack.transform.childCount;
        for (int i = 0; i < cnt; ++i)
        {
            objPlayPack.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetPlayerPos()
    {
        // 네브매쉬 때문에 정확한 이동이 되지 않아서..
        objPlayer.SetActive(false);

        switch (SceneCtrlManager.Instace.PrevSceneNo)
        {
            case E_SCENE_NO.ROBBY:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromRobby.position, tfFromRobby.rotation);
                }
                break;
            case E_SCENE_NO.DUNGEON:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromRobby.position, tfFromRobby.rotation);
                }
                break;
            default:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromRobby.position, tfFromRobby.rotation);
                }
                break;
        }

        objPlayer.SetActive(true);
    }
}
