using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDBManager
{
    private static QuestDBManager sInstance = null;
    public static QuestDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new QuestDBManager();
            }
            return sInstance;
        }
    }

    private quest questDB = null;

    public void Setup()
    {
        questDB = Resources.Load<quest>(Util.ResourcePath.DB_QUEST);
    }

    public string GetQuestContent(int index)
    {
        return questDB.dataArray[index].Content;
    }

    public int GetCoin(int index)
    {
        return questDB.dataArray[index].Coin;
    }

    public int GetMaxQuestNo()
    {
        return questDB.dataArray.Length;
    }
}
