using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDBManager
{
    private static EnemyDBManager sInstance = null;
    public static EnemyDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                //GameObject newObject = new GameObject("_LevelManager");
                sInstance = new EnemyDBManager();
            }
            return sInstance;
        }
    }

    enemy enemyDB = null;

    public void Setup()
    {
        enemyDB = Resources.Load<enemy>(Util.ResourcePath.DB_ENEMY);
    }

    public void SetStat(CharacterStat stat, E_CHARACTER_TYPE eType)
    {
        stat.fMaxHP = enemyDB.dataArray[(int)eType].Maxhp;
        stat.fMaxMP = enemyDB.dataArray[(int)eType].Maxmp;
        stat.fAtt = enemyDB.dataArray[(int)eType].Att;
        stat.fDef = enemyDB.dataArray[(int)eType].Def;
        stat.fMaxExp = enemyDB.dataArray[(int)eType].Exp;
        stat.nMoney = enemyDB.dataArray[(int)eType].Coin;
    }

    public string GetEnemyName(E_CHARACTER_TYPE eType)
    {
        return enemyDB.dataArray[(int)eType].Name;
    }
}
