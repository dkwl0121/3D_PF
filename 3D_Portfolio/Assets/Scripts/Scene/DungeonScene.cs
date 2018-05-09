using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : MonoBehaviour
{
    private GameObject objPlayPack;
    private GameObject objPlayer;

    public GameObject[] arrObjPos;

    public Transform PlayerPos;

    private void Awake()
    {
        objPlayPack = GameObject.FindGameObjectWithTag(Util.Tag.PLAY_PACK);
        int cnt = objPlayPack.transform.childCount;
        for (int i = 0; i < cnt; ++i)
        {
            objPlayPack.transform.GetChild(i).gameObject.SetActive(true);
        }
        
        objPlayer = objPlayPack.transform.Find(Util.Tag.PLAYER).gameObject;
        SetPlayerPos();

        SetEnemyPos();
    }

    private void OnDestroy()
    {
        if (SceneCtrlManager.Instace.NextSceneNo == E_SCENE_NO.TOWN)
        {
            int cnt = objPlayPack.transform.childCount;
            for (int i = 0; i < cnt; ++i)
            {
                objPlayPack.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void SetPlayerPos()
    {
        // 네브매쉬 때문에 정확한 이동이 되지 않아서..
        objPlayer.SetActive(false);
        objPlayer.transform.SetPositionAndRotation(PlayerPos.position, PlayerPos.rotation);
        objPlayer.SetActive(true);
    }

    private void SetEnemyPos()
    {
        for (int i = 0; i < arrObjPos.Length; ++i)
        {
            arrObjPos[i].GetComponent<EnemySpaceControl>().eEnemyNo = DungeonDBManager.Instace.GetCharacterType(i);
        }
    }
}
