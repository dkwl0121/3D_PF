using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShop : MonoBehaviour
{
    private E_SHOP_TAB eCurrTab;

    private weaponData[] arrWeaponData = null;
    private Slot[] arrWeaponSlot = null;

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
            LoadITemTab();
        }

        UpdateSelectRect();
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
                GameObject objSlot = Instantiate(Resources.Load(Util.ResourcePath.ICON_SLOT), tfWeaponListParent.transform) as GameObject; ;
                arrWeaponSlot[i] = objSlot.GetComponent<Slot>();
                arrWeaponSlot[i].nIndex = i;
                GameObject objIcon = Instantiate(Resources.Load(arrWeaponData[i].Iconloadpath), arrWeaponSlot[i].transform) as GameObject;
            }
        }
        ShowWeaponTab();
    }

    private void LoadITemTab()
    {
        //// 로드를 했다면
        //if (!arrIsTabLoad[(int)E_SHOP_TAB.WEAPON])
        //{


        //    // 한번만 로드
        //    arrIsTabLoad[(int)E_SHOP_TAB.ITEM] = true;
        //}
        ShowItemTab();
    }

    private void ShowWeaponTab()
    {
        objRectWeapon.SetActive(true);
        objRectItem.SetActive(false);

        arrWeaponSlot[nCurrSelNum].SelectOn();
    }

    private void ShowItemTab()
    {
        objRectWeapon.SetActive(false);
        objRectItem.SetActive(true);
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
            // 플레이어에 갯터 함수 만들기
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
                PlayerManager.Instace.AddMoney(-arrWeaponData[nCurrSelNum].Price);
                PlayerManager.Instace.AddWeapon(nCurrSelNum);
                // 보유 개수 업데이트
                UpdateHoldCount();
            }
        }
        else
        {

        }
    }

    public void SelectIcon(int index)
    {
        nCurrSelNum = index;
        UpdateSelectRect();
    }
}
