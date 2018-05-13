using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDBManager
{
    private static WeaponDBManager sInstance = null;
    public static WeaponDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new WeaponDBManager();
            }
            return sInstance;
        }
    }

    private weapon WeaponDB = null;

    public void Setup()
    {
        WeaponDB = Resources.Load<weapon>(Util.ResourcePath.DB_WEAPON);
    }

    public weaponData[] GetArrWeaponInfo()
    {
        return WeaponDB.dataArray;
    }

    public int GetMaxWeaponCount()
    {
        return WeaponDB.dataArray.Length;
    }
}
