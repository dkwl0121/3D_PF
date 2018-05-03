using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : MonoBehaviour
{
    public Transform tfFromRobby;
    //public Transform tfFromShop;
    //public Transform tfDungeon1;

    private GameObject objPlayer = null;

    private void Awake()
    {
        SceneManager.Instace.Setup();
        LevelDBManager.Instace.Setup();
        EnemyDBManager.Instace.Setup();
        FrameManager.Instace.Setup();
        PlayerManager.Instace.Setup();
        EnemyPool.Instace.Setup();

        objPlayer = GameObject.FindGameObjectWithTag(Util.Tag.PLAYER);
        
        SetPlayerPos();
    }

    private void SetPlayerPos()
    {
        // 네브매쉬 때문에 정확한 이동이 되지 않아서..
        objPlayer.SetActive(false);

        switch (SceneManager.Instace.PrevSceneNo)
        {
            case E_SCENE_NO.ROBBY:
                {
                    objPlayer.transform.SetPositionAndRotation(tfFromRobby.position, tfFromRobby.rotation);
                }
                break;
            case E_SCENE_NO.SHOP:
                {
                    //================================샵은 NPC로 씬 따로 안 뺄 예정
                    //objPlayer.transform.SetPositionAndRotation(tfFromShop.position, tfFromShop.rotation);
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
