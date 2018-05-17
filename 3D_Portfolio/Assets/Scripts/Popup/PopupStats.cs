using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStats : MonoBehaviour
{
    public Transform tfWeaponIconSlot;     // 아이콘 슬롯
    public Text txtWeaponName;
    public Text txtWeaponDescription;

    public Text txtAtt;
    public Text txtDef;
    public Text txtStr;
    public Text txtDex;
    public Text txtInt;

    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = true;

        SetState();
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = false;
    }

    private void SetState()
    {
        int CurrIndex = PlayerManager.Instace.CurrWeapon;
        CharacterStat PlayerStat = PlayerManager.Instace.Stat;

        GameObject objIcon = Instantiate(Resources.Load(
            WeaponDBManager.Instace.GetArrWeaponInfo()[CurrIndex].Iconloadpath),
            tfWeaponIconSlot.transform) as GameObject;
        txtWeaponName.text = WeaponDBManager.Instace.GetArrWeaponInfo()[CurrIndex].Name;

        // 무기 설명
        if (PlayerManager.Instace.WeaponData[CurrIndex].fPlusAtt != 0)
        {
            txtWeaponDescription.text =
                WeaponDBManager.Instace.GetArrWeaponInfo()[CurrIndex].Description
                + "<color=#00ff00ff> (+" + PlayerManager.Instace.WeaponData[CurrIndex].fPlusAtt + ")</color>";
        }
        else
        {
            txtWeaponDescription.text = WeaponDBManager.Instace.GetArrWeaponInfo()[CurrIndex].Description;
        }

        // ATT
        if(PlayerManager.Instace.GetCurrPlusAtt() > 1.0f)
            txtAtt.text = PlayerStat.fAtt + "<color=Green> (*" + PlayerManager.Instace.GetCurrPlusAtt() + ")</color>";
        else
            txtAtt.text = PlayerStat.fAtt.ToString();

        // DEF
        if (PlayerManager.Instace.GetCurrPlusDef() > 0)
            txtDef.text = PlayerStat.fDef + "<color=Green> (+" + PlayerManager.Instace.GetCurrPlusDef() + ")</color>";
        else
            txtDef.text = PlayerStat.fDef.ToString();

        // STR
        if (PlayerManager.Instace.GetCurrPlusStr() > 0)
            txtStr.text = PlayerStat.fStr + "<color=Green> (+" + PlayerManager.Instace.GetCurrPlusStr() + ")</color>";
        else
            txtStr.text = PlayerStat.fStr.ToString();

        // DEX
        txtDex.text = PlayerStat.fDex.ToString();

        // INT
        if (PlayerManager.Instace.GetCurrPlusInt() > 1.0f)
            txtInt.text = PlayerStat.fInt + "<color=Green> (*" + PlayerManager.Instace.GetCurrPlusInt() + ")</color>";
        else
            txtInt.text = PlayerStat.fInt.ToString();
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }
}
