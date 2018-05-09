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
[CustomEditor(typeof(dungeon))]
public class dungeonEditor : BaseGoogleEditor<dungeon>
{	    
    public override bool Load()
    {        
        dungeon targetData = target as dungeon;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<dungeonData>(targetData.WorksheetName) ?? db.CreateTable<dungeonData>(targetData.WorksheetName);
        
        List<dungeonData> myDataList = new List<dungeonData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            dungeonData data = new dungeonData();
            
            data = Cloner.DeepCopy<dungeonData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
