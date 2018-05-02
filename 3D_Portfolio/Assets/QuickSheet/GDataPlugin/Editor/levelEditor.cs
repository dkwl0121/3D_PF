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
[CustomEditor(typeof(level))]
public class levelEditor : BaseGoogleEditor<level>
{	    
    public override bool Load()
    {        
        level targetData = target as level;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<levelData>(targetData.WorksheetName) ?? db.CreateTable<levelData>(targetData.WorksheetName);
        
        List<levelData> myDataList = new List<levelData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            levelData data = new levelData();
            
            data = Cloner.DeepCopy<levelData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
