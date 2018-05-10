using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDungeon : MonoBehaviour
{
    public GameObject[] arrObjDunLock;

    private void Awake()
    {
        GameManager.Instace.Popup = true;
        
        // 현재 클리어 상태에 따라 오픈!!
        for (int i = 0; i < arrObjDunLock.Length; ++i)
        {
            if (i <= (int)PlayerManager.Instace.CurrDungeonNo)
                arrObjDunLock[i].SetActive(false);
            else
                arrObjDunLock[i].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instace.Popup = false;
    }

    public void ClickDungeon(int index)
    {
        DungeonDBManager.Instace.DungeonNo = (E_DUNGEON_NO)index;
        GameManager.Instace.GameStart = true;
    }

    public void ClosePopup()
    {
        Destroy(this.gameObject);
    }
}
