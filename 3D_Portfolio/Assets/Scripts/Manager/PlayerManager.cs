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

    CharacterStat PlayerStat;

    public void Setup()
    {
        PlayerStat = new CharacterStat();
    }

    public CharacterStat LoadData()
    {
        //if (PlayerPrefs.HasKey("nLevel"))
        //{
        //    PlayerStat.nLevel = PlayerPrefs.GetInt("nLevel");
        //    PlayerStat.fCurrHP = PlayerPrefs.GetInt("fCurrHP");
        //    PlayerStat.fCurrMP = PlayerPrefs.GetInt("fCurrMP");
        //    PlayerStat.fCurrExp = PlayerPrefs.GetInt("fCurrExp");

        //    // 레벨에 따른 스탯 설정
        //    LevelDBManager.Instace.SetStat(PlayerStat, PlayerStat.nLevel);
        //}
        //else
        //{
        PlayerStat.nLevel = 1;

        // 레벨에 따른 스탯 설정
        LevelDBManager.Instace.SetStat(PlayerStat, PlayerStat.nLevel);

        PlayerStat.fCurrHP = PlayerStat.fMaxHP;
        PlayerStat.fCurrMP = PlayerStat.fMaxMP;
        PlayerStat.fCurrExp = 0;
        //}

        return PlayerStat;
    }

    public void SaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("nLevel", PlayerStat.nLevel);
        PlayerPrefs.SetInt("fCurrHP", (int)PlayerStat.fCurrHP);
        PlayerPrefs.SetInt("fCurrMP", (int)PlayerStat.fCurrMP);
        PlayerPrefs.SetInt("fCurrExp", (int)PlayerStat.fCurrExp);

        PlayerPrefs.Save();
    }

    public void AddExp(float exp)
    {
        PlayerStat.fCurrExp += exp;
    }
}
