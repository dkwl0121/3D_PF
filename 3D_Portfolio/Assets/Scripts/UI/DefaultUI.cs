using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public Text txtCoin;
    //public Text txtDia;

    private void Update()
    {
        txtCoin.text = PlayerManager.Instace.Stat.nMoney.ToString();
        //txtDia.text = dia.ToString();
    }

    public void ClickSetting()
    {
        if (GameManager.Instace.NoMove) return;

        // 옵션창 설정
    }

    public void ClickExit()
    {
        if (GameManager.Instace.NoMove) return;

        Instantiate(Resources.Load(Util.ResourcePath.POPUP_EXIT));
    }
}
