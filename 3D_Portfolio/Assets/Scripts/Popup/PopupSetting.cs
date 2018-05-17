using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : MonoBehaviour
{
    public GameObject objBgmOn;
    public GameObject objBgmOff;
    public GameObject objEftOn;
    public GameObject objEftOff;
    public Slider sliderVolume;

    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        GameManager.Instace.NoMove = true;

        if (SoundManager.Instance.BgmOn)
        {
            objBgmOn.SetActive(true);
            objBgmOff.SetActive(false);
        }
        else
        {
            objBgmOn.SetActive(false);
            objBgmOff.SetActive(true);
        }

        if (SoundManager.Instance.EftOn)
        {
            objEftOn.SetActive(true);
            objEftOff.SetActive(false);
        }
        else
        {
            objEftOn.SetActive(false);
            objEftOff.SetActive(true);
        }

        sliderVolume.value = SoundManager.Instance.GetVolume() * sliderVolume.maxValue;
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        GameManager.Instace.NoMove = false;
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }

    public void ClickBgmButtom()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        if (objBgmOn.activeSelf)
        {
            SoundManager.Instance.BgmOn = true;
            switch (SceneCtrlManager.Instace.CurrSceneNo)
            {
                case E_SCENE_NO.ROBBY:
                    SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.ROBBY);
                    break;
                case E_SCENE_NO.TOWN:
                    SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.TOWN);
                    break;
                case E_SCENE_NO.DUNGEON:
                    SoundManager.Instance.PlayBgm(E_BGM_SOUND_LIST.DUNGEON);
                    break;
            }
        }
        else
        {
            SoundManager.Instance.StopBgm();
            SoundManager.Instance.BgmOn = false;
        }
    }

    public void ClickEftButtom()
    {
        if (objEftOn.activeSelf)
        {
            SoundManager.Instance.EftOn = true;
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);
        }
        else
        {
            SoundManager.Instance.EftOn = false;
        }
    }

    public void ChangeVolume()
    {
        SoundManager.Instance.SetBgmVolume(sliderVolume.value / sliderVolume.maxValue);
        SoundManager.Instance.SetEftVolume(sliderVolume.value / sliderVolume.maxValue);

        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);
    }
}
