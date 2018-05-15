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
[CustomEditor(typeof(item))]
public class itemEditor : BaseGoogleEditor<item>
{	    
    public override bool Load()
    {        
        item targetData = target as item;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<itemData>(targetData.WorksheetName) ?? db.CreateTable<itemData>(targetData.WorksheetName);
        
        List<itemData> myDataList = new List<itemData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            itemData data = new itemData();
            
            data = Cloner.DeepCopy<itemData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
