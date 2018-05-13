using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupExit : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instace.NoMove = true;
    }

    private void OnDestroy()
    {
        GameManager.Instace.NoMove = false;
    }

    public void ClickYes()
    {
        GameManager.Instace.Exit(SceneCtrlManager.Instace.CurrSceneNo);
        Destroy(this.gameObject);
    }

    public void ClickNo()
    {
        Destroy(this.gameObject);
    }
}
