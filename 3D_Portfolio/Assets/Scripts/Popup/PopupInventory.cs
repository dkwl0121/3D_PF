using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInventory : MonoBehaviour
{
    private E_INVENTORY_TAB eCurrTab;

    private weaponData[] arrWeaponData = null;
    private PlayerWeapon[] arrPlayerWeaponData = null;
    private Slot[] arrWeaponSlot = null;

    private itemData[] arrItemData = null;
    private int[] arrPlayerItemData = null;
    private Slot[] arrItemSlot = null;

    public Image imgWeaponTab;
    public Image imgItemTab;

    public GameObject objRectWeapon;
    public GameObject objRectItem;

    public Transform tfWeaponListParent;
    public Transform tfItemListParent;

    public GameObject objSelectRect;    // 선택창(Disable 시킬 오브젝트)
    public Transform tfSelIconSlot;     // 아이콘 슬롯
    public Text txtSelHoldCnt;          // 보유개수
    public Text txtSelName;
    public Text txtSelDescription;

    private int nCurrSelNum;            // 현재 선택 아이템 넘버

    private void Awake()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = true;

        objRectWeapon.SetActive(false);
        objRectItem.SetActive(false);

        // 기본은 무기탭으로 설정
        ClickTab(E_INVENTORY_TAB.WEAPON);
    }

    private void OnDestroy()
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.POPUP);

        GameManager.Instace.NoMove = false;
    }

    private void Update()
    {
        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            for (int i = 0; i < arrWeaponSlot.Length; ++i)
            {
                // 새로 선택 된 슬롯이면
                if (arrWeaponSlot[i] != null && arrWeaponSlot[i].Select && nCurrSelNum != arrWeaponSlot[i].nIndex)
                {
                    SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

                    arrWeaponSlot[i].SelectOn();
                    nCurrSelNum = arrWeaponSlot[i].nIndex;
                    // 다른 슬롯 해제
                    for (int j = 0; j < arrWeaponSlot.Length; ++j)
                    {
                        if (arrWeaponSlot[j].nIndex != nCurrSelNum)
                            arrWeaponSlot[j].SelectOff();
                    }
                    // 선택 아이템 업데이트
                    UpdateSelectRect();
                }
            }
        }
        else
        {
            for (int i = 0; i < arrItemSlot.Length; ++i)
            {
                // 새로 선택 된 슬롯이면
                if (arrItemSlot[i] != null && arrItemSlot[i].Select && nCurrSelNum != arrItemSlot[i].nIndex)
                {
                    SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

                    arrItemSlot[i].SelectOn();
                    nCurrSelNum = arrItemSlot[i].nIndex;
                    // 다른 슬롯 해제
                    for (int j = 0; j < arrItemSlot.Length; ++j)
                    {
                        if (arrItemSlot[j].nIndex != nCurrSelNum)
                            arrItemSlot[j].SelectOff();
                    }
                    // 선택 아이템 업데이트
                    UpdateSelectRect();
                }
            }
        }
    }

    private void ClickTab(E_INVENTORY_TAB eTab)
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

        eCurrTab = eTab;

        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            // 탭 색 변경
            imgWeaponTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_ACTIVE);
            imgItemTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_INACTIVE);
            LoadWeaponTab();
        }
        else
        {
            // 탭 색 변경
            imgWeaponTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_INACTIVE);
            imgItemTab.sprite = Resources.Load<Sprite>(Util.ResourcePath.IMAGE_TAB_ACTIVE);
            LoadItemTab();
        }
    }

    private void LoadWeaponTab()
    {
        // 로드를 안했다면
        if (arrWeaponData == null)
        {
            arrWeaponData = WeaponDBManager.Instace.GetArrWeaponInfo();
            arrPlayerWeaponData = PlayerManager.Instace.WeaponData;

            // 슬롯 배열 생성 및 무기 이미지 로드
            arrWeaponSlot = new Slot[arrWeaponData.Length];
            for (int i = 0; i < arrWeaponData.Length; ++i)
            {
                GameObject objSlot = Instantiate(Resources.Load(Util.ResourcePath.ICON_SLOT), tfWeaponListParent.transform) as GameObject;
                arrWeaponSlot[i] = objSlot.GetComponent<Slot>();
                arrWeaponSlot[i].nIndex = i;
                GameObject objIcon = Instantiate(Resources.Load(arrWeaponData[i].Iconloadpath), arrWeaponSlot[i].transform) as GameObject;
            }
        }
        ShowWeaponTab();
    }

    private void LoadItemTab()
    {
        // 로드를 안했다면
        if (arrItemData == null)
        {
            arrItemData = ItemDBManager.Instace.GetArrItemInfo();
            arrPlayerItemData = PlayerManager.Instace.ItemData;

            // 슬롯 배열 생성 및 무기 이미지 로드
            arrItemSlot = new Slot[arrItemData.Length];
            for (int i = 0; i < arrItemData.Length; ++i)
            {
                GameObject objSlot = Instantiate(Resources.Load(Util.ResourcePath.ICON_SLOT), tfItemListParent.transform) as GameObject;
                arrItemSlot[i] = objSlot.GetComponent<Slot>();
                arrItemSlot[i].nIndex = i;
                GameObject objIcon = Instantiate(Resources.Load(arrItemData[i].Iconloadpath), arrItemSlot[i].transform) as GameObject;
            }
        }
        ShowItemTab();
    }

    private void ShowWeaponTab()
    {
        objRectWeapon.SetActive(true);
        objRectItem.SetActive(false);

        for (int i = 0; i < arrWeaponSlot.Length; ++i)
        {
            // 인벤에 소유하고 있는 무기라면 활성화(현재 장착중이면 제외!)
            if (arrPlayerWeaponData[i].nCount != 0 && arrWeaponSlot[i].nIndex != PlayerManager.Instace.CurrWeapon)
                arrWeaponSlot[i].gameObject.SetActive(true);
            else
                arrWeaponSlot[i].gameObject.SetActive(false);
        }

        // 선택인덱스 설정하기
        UpdateCurrSelNum();
        UpdateSelectRect();
    }

    private void ShowItemTab()
    {
        objRectWeapon.SetActive(false);
        objRectItem.SetActive(true);

        for (int i = 0; i < arrItemSlot.Length; ++i)
        {
            // 인벤에 소유하고 있는 아이템이라면 활성화
            if (arrPlayerItemData[i] != 0)
                arrItemSlot[i].gameObject.SetActive(true);
            else
                arrItemSlot[i].gameObject.SetActive(false);
        }

        // 선택인덱스 설정하기
        UpdateCurrSelNum();
        UpdateSelectRect();
    }

    private void UpdateCurrSelNum()
    {
        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            nCurrSelNum = 0;
            bool isFind = false;
            for (int i = 0; i < arrWeaponSlot.Length; ++i)
            {
                if (!isFind && arrWeaponSlot[i].gameObject.activeSelf)
                {
                    nCurrSelNum = i;
                    isFind = true;
                }
                arrWeaponSlot[i].SelectOff();
            }
            arrWeaponSlot[nCurrSelNum].SelectOn();
        }
        else
        {
            nCurrSelNum = 0;
            bool isFind = false;
            for (int i = 0; i < arrItemSlot.Length; ++i)
            {
                if (!isFind && arrItemSlot[i].gameObject.activeSelf)
                {
                    nCurrSelNum = i;
                    isFind = true;
                }
                arrItemSlot[i].SelectOff();
            }
            arrItemSlot[nCurrSelNum].SelectOn();
        }
    }

    private void UpdateSelectRect()
    {
        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            // 슬롯에 이미 아이콘이 있다면 삭제하고 로드하기
            if (tfSelIconSlot.childCount > 0)
                Destroy(tfSelIconSlot.GetChild(0).gameObject);

            // 인벤에 소유하고 있는 무기가 있다면 활성화 (장착하고 있는 무기는 제외)
            if (arrPlayerWeaponData[nCurrSelNum].nCount != 0 && arrWeaponSlot[nCurrSelNum].nIndex != PlayerManager.Instace.CurrWeapon)
            {
                objSelectRect.SetActive(true);
                GameObject objIcon = Instantiate(Resources.Load(arrWeaponData[nCurrSelNum].Iconloadpath), tfSelIconSlot.transform) as GameObject;
                txtSelName.text = arrWeaponData[nCurrSelNum].Name;
                if (arrPlayerWeaponData[nCurrSelNum].fPlusAtt != 0)
                {
                    txtSelDescription.text =
                    arrWeaponData[nCurrSelNum].Description
                    + "<color=#00ff00ff>  (+" + arrPlayerWeaponData[nCurrSelNum].fPlusAtt + ")</color>";
                }
                else
                {
                    txtSelDescription.text = arrWeaponData[nCurrSelNum].Description;
                }
            }
            else
                objSelectRect.SetActive(false);
        }
        else
        {
            // 슬롯에 이미 아이콘이 있다면 삭제하고 로드하기
            if (tfSelIconSlot.childCount > 0)
                Destroy(tfSelIconSlot.GetChild(0).gameObject);

            // 인벤에 소유하고 있는 아이템이 있다면 활성화
            if (arrPlayerItemData[nCurrSelNum] != 0)
            {
                objSelectRect.SetActive(true);
                GameObject objIcon = Instantiate(Resources.Load(arrItemData[nCurrSelNum].Iconloadpath), tfSelIconSlot.transform) as GameObject;
                txtSelName.text = arrItemData[nCurrSelNum].Name;
                txtSelDescription.text = arrItemData[nCurrSelNum].Description;
            }
            else
                objSelectRect.SetActive(false);
           
        }
        // 보유개수 업데이트
        UpdateHoldCount();
    }

    private void UpdateHoldCount()
    {
        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            txtSelHoldCnt.text = PlayerManager.Instace.GetWeaponHoldCnt(nCurrSelNum).ToString();
        }
        else
        {
            txtSelHoldCnt.text = PlayerManager.Instace.GetItemHoldCnt(nCurrSelNum).ToString();
        }
    }

    public void ClickWeaponTab()
    {
        ClickTab(E_INVENTORY_TAB.WEAPON);
    }

    public void ClickItemTab()
    {
        ClickTab(E_INVENTORY_TAB.ITEM);
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }

    public void ClickUse()
    {
        if (eCurrTab == E_INVENTORY_TAB.WEAPON)
        {
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

            PlayerManager.Instace.UseWeapon(nCurrSelNum);
            
            // 보유 목록 업데이트
            ShowWeaponTab();
        }
        else
        {
            SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.SELECT);

            PlayerManager.Instace.UseItem(nCurrSelNum);

            // 사용한 아이템의 개수가 0이라면 -> 보유 목록 업데이트
            if (arrPlayerItemData[nCurrSelNum] == 0)
                ShowItemTab();
            // 아니면 보유 개수만 업데이트
            else
                UpdateHoldCount();
        }
    }
}