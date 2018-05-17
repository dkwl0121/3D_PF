using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLose : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.LOSE);

        GameManager.Instace.NoMove = true;
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
