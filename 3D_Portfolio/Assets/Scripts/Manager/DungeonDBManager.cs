using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDBManager
{
    private static DungeonDBManager sInstance = null;
    public static DungeonDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new DungeonDBManager();
            }
            return sInstance;
        }
    }

    private dungeon DungeonDB = null;

    // 현재 던전 넘버
    private E_DUNGEON_NO eDungeonNo = E_DUNGEON_NO.DUNGEON_01;
    public E_DUNGEON_NO DungeonNo { get { return eDungeonNo; } set { eDungeonNo = value; } }

    public void Setup()
    {
        DungeonDB = Resources.Load<dungeon>(Util.ResourcePath.DB_DUNGEON);
    }

    public E_CHARACTER_TYPE GetCharacterType(int posIndex)
    {
        switch (posIndex)
        {
            case 0:
                return DungeonDB.dataArray[(int)eDungeonNo].POS1;
            case 1:
                return DungeonDB.dataArray[(int)eDungeonNo].POS2;
            case 2:
                return DungeonDB.dataArray[(int)eDungeonNo].POS3;
            case 3:
                return DungeonDB.dataArray[(int)eDungeonNo].POS4;
            case 4:
                return DungeonDB.dataArray[(int)eDungeonNo].POS5;
            case 5:
                return DungeonDB.dataArray[(int)eDungeonNo].POS6;
            case 6:
                return DungeonDB.dataArray[(int)eDungeonNo].POS7;
            default:
                return E_CHARACTER_TYPE.INVALID;
        }
    }

    public int GetWinCoin()
    {
        return DungeonDB.dataArray[(int)eDungeonNo].Coin;
    }

    public float GetWinExp()
    {
        return DungeonDB.dataArray[(int)eDungeonNo].Exp;
    }

    public string GetEnemyName(int posIndex)
    {
        switch (posIndex)
        {
            case 0:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS1);
            case 1:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS2);
            case 2:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS3);
            case 3:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS4);
            case 4:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS5);
            case 5:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS6);
            case 6:
                return EnemyDBManager.Instace.GetEnemyName(DungeonDB.dataArray[(int)eDungeonNo].POS7);
            default:
                return null;
        }
    }
}
