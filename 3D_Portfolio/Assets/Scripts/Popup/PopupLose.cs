using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLose : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instace.NoMove = true;
    }

    private void OnDestroy()
    {
        GameManager.Instace.NoMove = false;
    }

    public void ClickOK()
    {
        GameManager.Instace.GameStart = false;
        Destroy(this.gameObject);
    }
}
