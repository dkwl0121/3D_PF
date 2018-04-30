using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private static LevelManager sInstance = null;
    public static LevelManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                //GameObject newObject = new GameObject("_LevelManager");
                sInstance = new LevelManager();
            }
            return sInstance;
        }
    }

    level levelDB = null;
    
    public void Setup()
    {
        levelDB = Resources.Load<level>("DB/Level");
    }

    public float GetHpByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Maxhp;
    }

    public float GetMpByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Maxmp;
    }

    public float GetExpByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Maxexp;
    }

    public float GetAttByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Att;
    }

    public float GetDefByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Def;
    }

    public float GetStrByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Str;
    }

    public float GetDexByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Dex;
    }

    public float GetIntByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].I;
    }

    public float GetUpHpByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Uphp;
    }

    public float GetUpMpByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Upmp;
    }

    public float GetCoolTimeByLevel(int level)
    {
        if (level <= 0 || level >= levelDB.dataArray.Length)
            return Util.NumValue.INVALID_NUMBER;

        return levelDB.dataArray[level - 1].Cooltime;
    }
}