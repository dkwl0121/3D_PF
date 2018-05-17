using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDBManager
{
    private static StoryDBManager sInstance = null;
    public static StoryDBManager Instace
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new StoryDBManager();
            }
            return sInstance;
        }
    }

    private story storyDB = null;

    public void Setup()
    {
        storyDB = Resources.Load<story>(Util.ResourcePath.DB_STORY);
    }

    public int GetStartIndex(int no)
    {
        for (int i = 0; i < storyDB.dataArray.Length; ++i)
        {
            if (storyDB.dataArray[i].No == no && storyDB.dataArray[i].Start)
                return i;
        }

        return Util.NumValue.INVALID_NUMBER;
    }
    
    public storyData GetStoryData(int index)
    {
        return storyDB.dataArray[index];
    }

    public int GetMaxStoryNo()
    {
        return storyDB.dataArray[storyDB.dataArray.Length - 1].No + 1;
    }
}
