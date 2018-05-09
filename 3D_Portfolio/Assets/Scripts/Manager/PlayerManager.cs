using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isNewGame;
    public bool NewGame { set { isNewGame = value; } }
    private bool isLevelUp;
    public bool Levelup { get { return isLevelUp; } set { isLevelUp = value; } }

    public void Setup()
    {
        PlayerStat = new CharacterStat();
    }

    public CharacterStat LoadData()
    {
        if (!isNewGame && PlayerPrefs.HasKey("nLevel"))
        {
            PlayerStat.nLevel = PlayerPrefs.GetInt("nLevel");
            PlayerStat.fCurrHP = PlayerPrefs.GetInt("fCurrHP");
            PlayerStat.fCurrMP = PlayerPrefs.GetInt("fCurrMP");
            PlayerStat.fCurrExp = PlayerPrefs.GetInt("fCurrExp");
            PlayerStat.nMoney = PlayerPrefs.GetInt("nMoney");

            // 전체적인 스텟 셋팅
            SetStat();
        }
        else
        {
            PlayerStat.nLevel = 6;

            // 전체적인 스텟 셋팅
            SetStat();

            PlayerStat.fCurrHP = PlayerStat.fMaxHP;
            PlayerStat.fCurrMP = PlayerStat.fMaxMP;
            PlayerStat.nMoney = 0;
            PlayerStat.fCurrExp = 0;
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

        PlayerPrefs.Save();
    }

    public void SetStat()
    {
        // 레벨에 따른 스탯 설정
        LevelDBManager.Instace.SetStat(PlayerStat, PlayerStat.nLevel);

        // 장착하고 있는 무기에 따라 추가 스탯 설정
    }

    public void AddExp(float exp)
    {
        PlayerStat.fCurrExp += exp;

        if (PlayerStat.fCurrExp >= PlayerStat.fMaxExp)
            LevelUp();
    }

    public void AddMoney(int money)
    {
        PlayerStat.nMoney += money;
    }

    private void LevelUp()
    {
        if (PlayerStat.nLevel >= LevelDBManager.Instace.GetMaxLevel())
        {
            PlayerStat.fCurrExp = PlayerStat.fMaxExp;
            return;
        }

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
}
