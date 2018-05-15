using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShop : MonoBehaviour
{
    private E_SHOP_TAB eCurrTab;

    private weaponData[] arrWeaponData = null;
    private Slot[] arrWeaponSlot = null;

    private itemData[] arrItemData = null;
    private Slot[] arrItemSlot = null;

    public Image imgWeaponTab;
    public Image imgItemTab;

    public GameObject objRectWeapon;
    public GameObject objRectItem;

    public Transform tfWeaponListParent;
    public Transform tfItemListParent;

    public Transform tfSelIconSlot;     // 아이콘 슬롯
    public Text txtSelHoldCnt;          // 보유개수
    public Text txtSelName;
    public Text txtSelDescription;
    public Text txtSelPrice;

    private int nCurrSelNum;            // 현재 선택 아이템 넘버

    private void Awake()
    {
        GameManager.Instace.NoMove = true;

        objRectWeapon.SetActive(false);
        objRectItem.SetActive(false);
        
        // 기본은 무기탭으로 설정
        ClickTab(E_SHOP_TAB.WEAPON);
    }

    private void OnDestroy()
    {
        GameManager.Instace.NoMove = false;
    }

    private void Update()
    {
        if (eCurrTab == E_SHOP_TAB.WEAPON)
        {
            for (int i = 0; i < arrWeaponSlot.Length; ++i)
            {
                // 새로 선택 된 슬롯이면
                if (arrWeaponSlot[i].Select && nCurrSelNum != arrWeaponSlot[i].nIndex)
                {
                    arrWeaponSlot[i].SelectOn();
                    nCurrSelNum = arrWeaponSlot[i].nIndex;
                    // 다른 슬롯 해제
                    for (int j = 0; j < arrWeaponSlot.Length; ++j)
                    {
                        if (j != nCurrSelNum)
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
                if (arrItemSlot[i].Select && nCurrSelNum != arrItemSlot[i].nIndex)
                {
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
    
    private void ClickTab(E_SHOP_TAB eTab)
    {
        eCurrTab = eTab;
        nCurrSelNum = 0;    // 초기화

        if (eCurrTab == E_SHOP_TAB.WEAPON)
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
        
        arrWeaponSlot[nCurrSelNum].SelectOn();
        UpdateSelectRect();
    }

    private void ShowItemTab()
    {
        objRectWeapon.SetActive(false);
        objRectItem.SetActive(true);
        
        arrItemSlot[nCurrSelNum].SelectOn();
        UpdateSelectRect();
    }

    private void UpdateSelectRect()
    {
        if (eCurrTab == E_SHOP_TAB.WEAPON)
        {
            // 슬롯에 이미 아이콘이 있다면 삭제하고 로드하기
            if (tfSelIconSlot.childCount > 0)
                Destroy(tfSelIconSlot.GetChild(0).gameObject);

            GameObject objIcon = Instantiate(Resources.Load(arrWeaponData[nCurrSelNum].Iconloadpath), tfSelIconSlot.transform) as GameObject;
            txtSelName.text = arrWeaponData[nCurrSelNum].Name;
            txtSelDescription.text = arrWeaponData[nCurrSelNum].Description;
            txtSelPrice.text = arrWeaponData[nCurrSelNum].Price.ToString();
        }
        else
        {
            // 슬롯에 이미 아이콘이 있다면 삭제하고 로드하기
            if (tfSelIconSlot.childCount > 0)
                Destroy(tfSelIconSlot.GetChild(0).gameObject);

            GameObject objIcon = Instantiate(Resources.Load(arrItemData[nCurrSelNum].Iconloadpath), tfSelIconSlot.transform) as GameObject;
            txtSelName.text = arrItemData[nCurrSelNum].Name;
            txtSelDescription.text = arrItemData[nCurrSelNum].Description;
            txtSelPrice.text = arrItemData[nCurrSelNum].Price.ToString();
        }
        // 보유개수 업데이트
        UpdateHoldCount();
    }

    private void UpdateHoldCount()
    {
        if (eCurrTab == E_SHOP_TAB.WEAPON)
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
        ClickTab(E_SHOP_TAB.WEAPON);
    }

    public void ClickItemTab()
    {
        ClickTab(E_SHOP_TAB.ITEM);
    }

    public void ClickClose()
    {
        Destroy(this.gameObject);
    }

    public void ClickBuy()
    {
        if (eCurrTab == E_SHOP_TAB.WEAPON)
        {
            if (arrWeaponData[nCurrSelNum].Price <= PlayerManager.Instace.Stat.nMoney)
            {
                if (!PlayerManager.Instace.IsHoldWeapon(nCurrSelNum))    // 무기를 소유하고 있지 않으면
                {
                    PlayerManager.Instace.AddMoney(-arrWeaponData[nCurrSelNum].Price);
                    PlayerManager.Instace.AddWeapon(nCurrSelNum);
                    // 보유 개수 업데이트
                    UpdateHoldCount();
                }
                // 이미 무기를 가지고 있다면
                else
                {
                    GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
                    objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.HAD_WEAPON);
                }
            }
            // 돈이모자라면
            else
            {
                GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
                objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.NO_MONEY);
            }
        }
        else
        {
            if (arrItemData[nCurrSelNum].Price <= PlayerManager.Instace.Stat.nMoney)
            {
                PlayerManager.Instace.AddMoney(-arrItemData[nCurrSelNum].Price);
                PlayerManager.Instace.AddItem(nCurrSelNum);
                // 보유 개수 업데이트
                UpdateHoldCount();
            }
            // 돈이모자라면
            else
            {
                GameObject objOkPopup = Instantiate(Resources.Load(Util.ResourcePath.POPUP_OK)) as GameObject;
                objOkPopup.GetComponent<PopupOk>().SetDescription(Util.Message.NO_MONEY);
            }
        }
    }
}
