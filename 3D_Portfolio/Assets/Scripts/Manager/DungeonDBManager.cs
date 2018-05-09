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

    private int nDungeonNo = 2;
    public int DungeonNo { set { nDungeonNo = value; } }

    public void Setup()
    {
        DungeonDB = Resources.Load<dungeon>(Util.ResourcePath.DB_DUNGEON);
    }

    public E_CHARACTER_TYPE GetCharacterType(int posIndex)
    {
        switch (posIndex)
        {
            case 0:
                return DungeonDB.dataArray[nDungeonNo].POS1;
            case 1:
                return DungeonDB.dataArray[nDungeonNo].POS2;
            case 2:
                return DungeonDB.dataArray[nDungeonNo].POS3;
            case 3:
                return DungeonDB.dataArray[nDungeonNo].POS4;
            case 4:
                return DungeonDB.dataArray[nDungeonNo].POS5;
            case 5:
                return DungeonDB.dataArray[nDungeonNo].POS6;
            case 6:
                return DungeonDB.dataArray[nDungeonNo].POS7;
            default:
                return E_CHARACTER_TYPE.INVALID;
        }
    }
}
