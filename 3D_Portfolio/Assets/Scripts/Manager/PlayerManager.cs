using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    public int nCount = 0;
    public float fPlusAtt = 0;
    public float fPlusInt = 0;
}

public class PlayerHealth
{
    public int nPlusDef = 0;
    public int nPlusStr = 0;
}

public class PlayerManager
{
    private static PlayerManager sInstance = null;
    public static PlayerManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new PlayerManager();
            }
            return sInstance;
        }
    }

    private CharacterStat PlayerStat;
    public CharacterStat Stat { get { return PlayerStat; } }

    private E_DUNGEON_NO eCurrDungeonNo;
    public E_DUNGEON_NO CurrDungeonNo { get { return eCurrDungeonNo; } }

    private PlayerHealth cHealth;
    public PlayerHealth Healt { get { return cHealth; } }

    private PlayerWeapon[] arrWeapon;
    public PlayerWeapon[] WeaponData { get { return arrWeapon; } }
    public int GetWeaponHoldCnt(int index) { return arrWeapon[index].nCount; }
    private int nCurrWeapon;
    public int CurrWeapon { get { return nCurrWeapon; } }
    private bool isChangeWeapon;
    public bool ChangeWeapon { get { return isChangeWeapon; } set { isChangeWeapon = value; } }

    public float GetCurrPlusAtt() { return WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrWeapon].Att + arrWeapon[nCurrWeapon].fPlusAtt; }
    public float GetCurrPlusInt() { return WeaponDBManager.Instace.GetArrWeaponInfo()[nCurrWeapon].Inte + arrWeapon[nCurrWeapon].fPlusInt; }
    public float GetCurrPlusDef() { return cHealth.nPlusDef; }
    public float GetCurrPlusStr() { return cHealth.nPlusStr; }

    private int[] arrItem;
    public int[] ItemData { get { return arrItem; } }
    public int GetItemHoldCnt(int index) { return arrItem[index]; }

    private bool isNewGame;
    public bool NewGame { set { isNewGame = value; } }

    private int nQuestNo;
    public int CurrQuestNo { get { return nQuestNo; } set { nQuestNo = value; } }

    private bool isClearQuest;
    public bool ClearQuest { get { return isClearQuest; } set { isClearQuest = value; } }
    
    private int nStoryNo;
    public int CurrStoryNo { get { return nStoryNo; } set { nStoryNo = value; } }

    private bool isLevelUp;
    public bool Levelup { get { return isLevelUp; } set { isLevelUp = value; } }
    
    public void Setup()
    {
        PlayerStat = new CharacterStat();

        cHealth = new PlayerHealth();
        arrWeapon = new PlayerWeapon[WeaponDBManager.Instace.GetMaxWeaponCount()];
        for (int i = 0; i < arrWeapon.Length; ++i)
        {
            arrWeapon[i] = new PlayerWeapon();
        }
        arrItem = new int[ItemDBManager.Instace.GetMaxItemCount()];

        // 무기 장착 하기
        isChangeWeapon = true;
    }

    public CharacterStat LoadData()
    {
        if (!isNewGame && PlayerPrefs.HasKey("nLevel"))
        {
            PlayerStat.nLevel = PlayerPrefs.GetInt("nLevel");

            // 레벨에 따른 스텟 셋팅
            SetStat();

            PlayerStat.fCurrHP = PlayerPrefs.GetInt("fCurrHP");
            PlayerStat.fCurrMP = PlayerPrefs.GetInt("fCurrMP");
            PlayerStat.fCurrExp = PlayerPrefs.GetInt("fCurrExp");
            PlayerStat.nMoney = PlayerPrefs.GetInt("nMoney");
            eCurrDungeonNo = (E_DUNGEON_NO)PlayerPrefs.GetInt("eCurrDungeonNo");
            cHealth.nPlusDef = PlayerPrefs.GetInt("nPlusDef");
            cHealth.nPlusStr = PlayerPrefs.GetInt("nPlusStr");
            nCurrWeapon = PlayerPrefs.GetInt("nCurrWeapon");
            for (int i = 0; i < arrWeapon.Length; ++i)
            {
                arrWeapon[i].nCount = PlayerPrefs.GetInt("weapon_nCount" + i);
                arrWeapon[i].fPlusAtt = float.Parse(PlayerPrefs.GetString("weapon_fPlusAtt" + i));
                arrWeapon[i].fPlusInt = float.Parse(PlayerPrefs.GetString("weapon_fPlusInt" + i));
            }
            for (int i = 0; i < arrItem.Length; ++i)
            {
                arrItem[i] = PlayerPrefs.GetInt("item" + i);
            }
            nQuestNo = PlayerPrefs.GetInt("nQuestNo");
            nStoryNo = PlayerPrefs.GetInt("nStoryNo");
            isClearQuest = PlayerPrefs.GetInt("isClearQuest") == 1 ? true : false;
        }
        else
        {
            PlayerStat.nLevel = 1;

            // 레벨에 따른 스텟 셋팅
            SetStat();

            PlayerStat.fCurrHP = PlayerStat.fMaxHP;
            PlayerStat.fCurrMP = PlayerStat.fMaxMP;

            PlayerStat.nMoney = 0;
            PlayerStat.fCurrExp = 0;
            eCurrDungeonNo = E_DUNGEON_NO.DUNGEON_01;
            cHealth.nPlusDef = 0;
            cHealth.nPlusStr = 0;
            nCurrWeapon = 0;
            for (int i = 0; i < arrWeapon.Length; ++i)
            {
                arrWeapon[i].nCount = 0;
                arrWeapon[i].fPlusAtt = 0;
                arrWeapon[i].fPlusInt = 0;
            }
            // 기본 무기는 가지고 있음.
            arrWeapon[0].nCount = 1;
            for (int i = 0; i < arrItem.Length; ++i)
            {
                arrItem[i] = 0;
            }
            nQuestNo = -1;
            nStoryNo = 0;
            isClearQuest = true;    // 초반엔 퀘스트가 없으므로 클리어 상태!
        }

        return PlayerStat;
    }

    public void SaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("nLevel", PlayerStat.nLevel);
        PlayerPrefs.SetInt("fCurrHP", (int)PlayerStat.fCurrHP);
        PlayerPrefs.SetInt("fCurrMP", (int)PlayerStat.fCurrMP);
        PlayerPrefs.SetInt("fCurrExp", (int)PlayerStat.fCurrExp);
        PlayerPrefs.SetInt("nMoney", (int)PlayerStat.nMoney);
        PlayerPrefs.SetInt("eCurrDungeonNo", (int)eCurrDungeonNo);
        PlayerPrefs.SetInt("nPlusDef", cHealth.nPlusDef);
        PlayerPrefs.SetInt("nPlusStr", cHealth.nPlusStr);
        PlayerPrefs.SetInt("nCurrWeapon", nCurrWeapon);
        for (int i = 0; i < arrWeapon.Length; ++i)
        {
            PlayerPrefs.SetInt("weapon_nCount" + i, arrWeapon[i].nCount);
            PlayerPrefs.SetString("weapon_fPlusAtt" + i, arrWeapon[i].fPlusAtt.ToString());
            PlayerPrefs.SetString("weapon_fPlusInt" + i, arrWeapon[i].fPlusInt.ToString());
        }
        for (int i = 0; i < arrItem.Length; ++i)
        {
            PlayerPrefs.SetInt("item" + i, arrItem[i]);
        }
        PlayerPrefs.SetInt("nQuestNo", nQuestNo);
        PlayerPrefs.SetInt("nStoryNo", nStoryNo);
        PlayerPrefs.SetInt("isClearQuest", isClearQuest == true ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void SetStat()
    {
        // 레벨에 따른 스탯 설정
        LevelDBManager.Instace.SetStat(PlayerStat, PlayerStat.nLevel);
    }

    public void AddHp(float hp)
    {
        PlayerStat.fCurrHP += hp;
        PlayerStat.fCurrHP = Mathf.Clamp(PlayerStat.fCurrHP, 0.0f, PlayerStat.fMaxHP);
    }

    public void AddMp(float mp)
    {
        PlayerStat.fCurrMP += mp;
        PlayerStat.fCurrMP = Mathf.Clamp(PlayerStat.fCurrMP, 0.0f, PlayerStat.fMaxMP);
    }

    public void AddExp(float exp)
    {
        PlayerStat.fCurrExp += exp;

        if (PlayerStat.fCurrExp >= PlayerStat.fMaxExp)
            LevelUp();
    }

    public void AddMoney(int money)
    {
        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.COIN);

        PlayerStat.nMoney += money;
    }

    private void LevelUp()
    {
        if (PlayerStat.nLevel >= LevelDBManager.Instace.GetMaxLevel())
        {
            PlayerStat.fCurrExp = PlayerStat.fMaxExp;
            return;
        }

        SoundManager.Instance.PlayEfx(E_EFT_SOUND_LIST.PLAYER_LEVEL_UP);

        // 넘는 Exp
        float fNextExp = PlayerStat.fCurrExp - PlayerStat.fMaxExp;
        PlayerStat.fCurrExp = 0;

        isLevelUp = true;
        PlayerStat.nLevel += 1;
        
        // 전체 스텟 셋팅
        SetStat();

        PlayerStat.fCurrHP = PlayerStat.fMaxHP;
        PlayerStat.fCurrMP = PlayerStat.fMaxMP;

        // Exp 정리
        AddExp(fNextExp);
    }

    public int GetCurrLevel()
    {
        return PlayerStat.nLevel;
    }

    public void ClearDungeon(E_DUNGEON_NO eDun)
    {
        if (eDun < eCurrDungeonNo || eDun == E_DUNGEON_NO.MAX - 1) return;

        ++eCurrDungeonNo;
    }

    public void SetWin()
    {
        AddMoney(DungeonDBManager.Instace.GetWinCoin());
        AddExp(DungeonDBManager.Instace.GetWinExp());
    }

    // 무기 소유 여부
    public bool IsHoldWeapon(int index)
    {
        return arrWeapon[index].nCount != 0;
    }

    public void AddWeapon(int index)
    {
        arrWeapon[index].nCount += 1;

        // 바로 장착하기.
        UseWeapon(index);
    }

    public void AddItem(int index)
    {
        arrItem[index] += 1;
    }

    public void ReinforceWeapon(int index, float plus)
    {
        arrWeapon[index].fPlusAtt += plus;
        arrWeapon[index].fPlusInt += plus;
    }

    public void ReinforceDefence(int plus)
    {
        cHealth.nPlusDef += plus;
    }

    public void ReinforceStrong(int plus)
    {
        cHealth.nPlusStr += plus;
    }

    public void UseWeapon(int index)
    {
        nCurrWeapon = index;

        // 무기 장착 하기
        isChangeWeapon = true;
    }

    public void UseItem(int index)
    {
        arrItem[index] -= 1;
        AddHp(ItemDBManager.Instace.GetArrItemInfo()[index].Hp);
        AddMp(ItemDBManager.Instace.GetArrItemInfo()[index].Mp);
    }
}
