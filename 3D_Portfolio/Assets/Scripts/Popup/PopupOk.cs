using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOk : MonoBehaviour
{
    public Text txtDescription;

    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);
    }

    public void SetDescription(string str)
    {
        txtDescription.text = str;
    }

    public void ClickOk()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        Destroy(this.gameObject);
    }
}
