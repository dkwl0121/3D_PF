using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDBManager
{
    private static LevelDBManager sInstance = null;
    public static LevelDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                //GameObject newObject = new GameObject("_LevelManager");
                sInstance = new LevelDBManager();
            }
            return sInstance;
        }
    }

    level levelDB = null;

    public void Setup()
    {
        levelDB = Resources.Load<level>(Util.ResourcePath.DB_LEVEL);
    }

    public int GetMaxLevel()
    {
        return levelDB.dataArray.Length;
    }

    public void SetStat(CharacterStat stat, int level)
    {
        if (level <= 0 || level > levelDB.dataArray.Length) return;

        stat.fMaxHP = levelDB.dataArray[level - 1].Maxhp;
        stat.fMaxMP = levelDB.dataArray[level - 1].Maxmp;
        stat.fMaxExp = levelDB.dataArray[level - 1].Maxexp;
        stat.fAtt = levelDB.dataArray[level - 1].Att;
        stat.fDef = levelDB.dataArray[level - 1].Def;
        stat.fStr = levelDB.dataArray[level - 1].Str;
        stat.fDex = levelDB.dataArray[level - 1].Dex;
        stat.fInt = levelDB.dataArray[level - 1].Inte;
        stat.fUpHp = levelDB.dataArray[level - 1].Uphp;
        stat.fUpMp = levelDB.dataArray[level - 1].Upmp;
        stat.fCoolTime = levelDB.dataArray[level - 1].Cooltime;
    }

    public string GetLevelUpTip(int level)
    {
        return levelDB.dataArray[level - 1].Leveluptip;
    }
}