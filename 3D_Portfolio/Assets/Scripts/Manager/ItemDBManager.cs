using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDBManager
{
    private static ItemDBManager sInstance = null;
    public static ItemDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new ItemDBManager();
            }
            return sInstance;
        }
    }

    private item ItemDB = null;

    public void Setup()
    {
        ItemDB = Resources.Load<item>(Util.ResourcePath.DB_ITEM);
    }

    public itemData[] GetArrItemInfo()
    {
        return ItemDB.dataArray;
    }

    public int GetMaxItemCount()
    {
        return ItemDB.dataArray.Length;
    }
}
