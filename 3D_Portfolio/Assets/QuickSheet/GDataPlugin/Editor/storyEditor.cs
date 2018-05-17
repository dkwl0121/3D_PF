using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(story))]
public class storyEditor : BaseGoogleEditor<story>
{	    
    public override bool Load()
    {        
        story targetData = target as story;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<storyData>(targetData.WorksheetName) ?? db.CreateTable<storyData>(targetData.WorksheetName);
        
        List<storyData> myDataList = new List<storyData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            storyData data = new storyData();
            
            data = Cloner.DeepCopy<storyData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
