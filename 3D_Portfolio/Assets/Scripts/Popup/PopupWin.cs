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
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.WIN);

        GameManager.Instace.NoMove = true;

        // 던전 넘버에 맞는 데이터 가져오기
        txtCoin.text = "+ " + DungeonDBManager.Instace.GetWinCoin().ToString();
        txtExp.text = "+ " + DungeonDBManager.Instace.GetWinExp().ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instace.NoMove = false;
    }

    public void ClickOK()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        GameManager.Instace.GameStart = false;
        Destroy(this.gameObject);
    }
}
