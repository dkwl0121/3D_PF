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
[CustomEditor(typeof(quest))]
public class questEditor : BaseGoogleEditor<quest>
{	    
    public override bool Load()
    {        
        quest targetData = target as quest;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<questData>(targetData.WorksheetName) ?? db.CreateTable<questData>(targetData.WorksheetName);
        
        List<questData> myDataList = new List<questData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            questData data = new questData();
            
            data = Cloner.DeepCopy<questData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
