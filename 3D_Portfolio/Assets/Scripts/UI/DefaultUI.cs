using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    public Text txtCoin;

    public GameObject objButtonList;

    private GameObject objMiniMapCamera;

    private void Awake()
    {
        objButtonList.SetActive(false);
    }

    private void OnEnable()
    {
        objMiniMapCamera = GameObject.FindGameObjectWithTag(Util.Tag.MINI_MAP_CAMERA);
        objMiniMapCamera.SetActive(false);
    }

    private void Update()
    {
        txtCoin.text = PlayerManager.Instace.Stat.nMoney.ToString();
    }

    public void ClickButtonGroup()
    {
        if (objButtonList.activeSelf)
            objButtonList.SetActive(false);
        else
            objButtonList.SetActive(true);
    }

    public void ClickInventory()
    {
        if (GameManager.Instace.NoMove) return;

        ClickButtonGroup();
        Instantiate(Resources.Load(Util.ResourcePath.POPUP_INVENTORY));
    }

    public void ClickStats()
    {
        if (GameManager.Instace.NoMove) return;

        ClickButtonGroup();
        Instantiate(Resources.Load(Util.ResourcePath.POPUP_STATS));
    }

    public void ClickMap()
    {
        ClickButtonGroup();

        if (objMiniMapCamera.activeSelf)
            objMiniMapCamera.SetActive(false);
        else
            objMiniMapCamera.SetActive(true);
    }

    public void ClickSetting()
    {
        if (GameManager.Instace.NoMove) return;

        ClickButtonGroup();
        Instantiate(Resources.Load(Util.ResourcePath.POPUP_SETTING));
    }

    public void ClickExit()
    {
        if (GameManager.Instace.NoMove) return;

        ClickButtonGroup();
        Instantiate(Resources.Load(Util.ResourcePath.POPUP_EXIT));
    }
}
