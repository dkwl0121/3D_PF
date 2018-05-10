using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : MonoBehaviour
{
    public Text txtCoin;
    public Text txtExp;

    private void Awake()
    {
        GameManager.Instace.Popup = true;

        // 던전 넘버에 맞는 데이터 가져오기
        txtCoin.text = "+ " + DungeonDBManager.Instace.GetWinCoin().ToString();
        txtExp.text = "+ " + DungeonDBManager.Instace.GetWinExp().ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instace.Popup = false;
    }

    public void ClickOK()
    {
        GameManager.Instace.GameStart = false;
        Destroy(this.gameObject);
    }
}
