using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupReinforce : MonoBehaviour
{
    private E_REINFORCE_TAB eCurrTab;

    private PlayerWeapon[] arrWeaponData = null;
    private Slot[] arrWeaponSlot = null;

    private PlayerHealth Health = null;

    public Image imgWeaponTab;
    public Image imgStrengthTab;

    public GameObject objRectWeapon;
    public GameObject objRectStrength;

    public Transform tfWeaponListParent;
    public Transform tfSelIconSlot;     // 아이콘 슬롯
    public Text txtSelName;
    public Text txtSelBeforeValue;
    public Text txtSelAfterValue;
    public Text txtSelPrice;

    private int nCurrSelNum;            // 현재 선택 아이템 넘버

    public Text txtDefBefore;
    public Text txtDefAfter;
    public Text txtDefPrice;

    public Text txtStrBefore;
    public Text txtStrAfter;
    public Text txtStrPrice;

    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = true;

        objRectWeapon.SetActive(false);
        objRectStrength.SetActive(false);
        
        // 기본은 무기탭으로 설정
        ClickTab(E_REINFORCE_TAB.WEAPON);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = false;
    }

    private void Update()
    {
        if (eCurrTab == E_REINFORCE_TAB.WEAPON)
        {
            for (int i = 0; i < arrWeaponSlot.Length; ++i)
            {
                // 새로 선택 된 슬롯이면
                if (arrWeaponData[i].nCount != 0 && arrWeaponSlot[i].Select && nCurrSelNum != arrWeaponSlot[i].nIndex)
                {
                    SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

                    arrWeaponSlot[i].SelectOn();
                    nCurrSelNum = arrWeaponSlot[i].nIndex;
                    // 다른 슬롯 해제
                    for (int j = 0; j < arrWeaponSlot.Length; ++j)
                    {
                        if (arrWeaponSlot[j] != null && arrWeaponSlot[j].nIndex != nCurrSelNum)
                            arrWeaponSlot[j].SelectOff();
                    }
                    // 선택 아이템 업데이트
                    UpdateSelectRect();
                }
            }
        }
    }
    
    private void ClickTab(E_REINFORCE_TAB eTab)
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        eCurrTab = eTab;

        if (eCurrTab == E_REINFORCE_TAB.WEAPON)
        {
            nCurrSelNum = 0;    // 초기화

            // 탭 색 변경
            imgWeaponTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_ACTIVE);
            imgStrengthTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_INACTIVE);
            LoadWeaponTab();
        }
        else
        {
            // 탭 색 변경
            imgWeaponTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_INACTIVE);
            imgStrengthTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_ACTIVE);
            LoadHealthTab();
        }
    }

    private void LoadWeaponTab()
    {
        // 로드를 안했다면
        if (arrWeaponData == null)
        {
            arrWeaponData = PlayerManager.Instace.WeaponData;

            // 슬롯 배열 생성 및 무기 이미지 로드
            arrWeaponSlot = new Slot[arrWeaponData.Length];
            for (int i = 0; i < arrWeaponData.Length; ++i)
            {
                // 소유하고 있지 않은 무기라면
                if (arrWeaponData[i].nCount == 0) continue;

                GameObject objSlot = Instantiate(Resources.Load(Util.ResourcePath.ICON_SLOT), tfWeaponListParent.transform) as GameObject;
                arrWeaponSlot[i] = objSlot.GetComponent<Slot>();
                arrWeaponSlot[i].nIndex = i;
                GameObject objIcon = Instantiate(Resources.Load(WeaponDBManager.Instace.GetArrWeaponInfo()[i].Iconloadpath),
                    arrWeaponSlot[i].transform) as GameObject;
            }
        }
        ShowWeaponTab();
    }

    private void LoadHealthTab()
    {
        // 로드를 안했다면
        if (Health == null)
        {
            Health = PlayerManager.Instace.Healt;
        }
        ShowHealthTab();
    }

    private void ShowWeaponTab()
    {
        objRectWeapon.SetActive(true);
        objRectStrength.SetActive(false);

        arrWeaponSlot[nCurrSelNum].SelectOn();
        UpdateSelectRect();
    }

    private void ShowHealthTab()
    {
        objRectWeapon.SetActive(false);
        objRectStrength.SetActive(true);

        UpdateHealthInfo();
    }

    private void UpdateSelectRect()
    {
        // 슬롯에 이미 아이콘이 있다면 삭제하고 로드하기
        if (tfSelIconSlot.childCount > 0)
            Destroy(tfSelIconSlot.GetChild(0).gameObject);

        GameObject objIcon = Instantiate(Resources.Load(WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Iconloadpath),
            tfSelIconSlot.transform) as GameObject;
        txtSelName.text = WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Name;
        float fWeaponAtt = WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Att + arrWeaponData[nCurrSelNum].fPlusAtt;
        txtSelBeforeValue.text = fWeaponAtt.ToString();
        txtSelAfterValue.text = (fWeaponAtt + 0.05f).ToString();
        txtSelPrice.text = (WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Price / 10).ToString();
    }

    private void UpdateHealthInfo()
    {
        UpdateDefenceInfo();
        UpdateStrongInfo();
    }

    private void UpdateDefenceInfo()
    {
        int CurrDef = (int)PlayerManager.Instace.Stat.fDef + Health.nPlusDef;
        txtDefBefore.text = CurrDef.ToString();
        txtDefAfter.text = (CurrDef + 10).ToString();
        txtDefPrice.text = "2000";
    }

    private void UpdateStrongInfo()
    {
        int CurrStr = (int)PlayerManager.Instace.Stat.fStr + Health.nPlusStr;
        txtStrBefore.text = CurrStr.ToString();
        txtStrAfter.text = (CurrStr + 5).ToString();
        txtStrPrice.text = "1000";
    }

    public void ClickWeaponTab()
    {
        ClickTab(E_REINFORCE_TAB.WEAPON);
    }

    public void ClickStrengthTab()
    {
        ClickTab(E_REINFORCE_TAB.STRENGTH);
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }

    public void ClickReinforceWeapon()
    {
        if (WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Price / 10 <= PlayerManager.Instace.Stat.nMoney)
        {
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.COIN);

            PlayerManager.Instace.AddMoney(-WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrSelNum].Price / 10);
            PlayerManager.Instace.ReinforceWeapon(nCurrSelNum, 0.05f);
            // 선택 렉트 업데이트
            UpdateSelectRect();
        }
        // 돈이모자라면
        else
        {
            GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
            objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.NO_MONEY);
        }
    }

    public void ClickReinforceDefence()
    {
        if (2000 <= PlayerManager.Instace.Stat.nMoney)
        {
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.COIN);

            PlayerManager.Instace.AddMoney(-2000);
            PlayerManager.Instace.ReinforceDefence(10);

            UpdateDefenceInfo();

            // 퀘스트 성공
            if (PlayerManager.Instace.CurrQuestNo == (int)E_QUEST_LIST.REINFORCE)
            {
                GameManager.Instace.QuestChange = true;
                PlayerManager.Instace.ClearQuest = true;
            }
        }
        // 돈이모자라면
        else
        {
            GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
            objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.NO_MONEY);
        }
    }

    public void ClickReinforceStrength()
    {
        if (1000 <= PlayerManager.Instace.Stat.nMoney)
        {
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.COIN);

            PlayerManager.Instace.AddMoney(-1000);
            PlayerManager.Instace.ReinforceStrong(5);

            UpdateStrongInfo();

            // 퀘스트 성공
            if (PlayerManager.Instace.CurrQuestNo == (int)E_QUEST_LIST.REINFORCE)
            {
                GameManager.Instace.QuestChange = true;
                PlayerManager.Instace.ClearQuest = true;
            }
        }
        // 돈이모자라면
        else
        {
            GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
            objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.NO_MONEY);
        }
    }
}
