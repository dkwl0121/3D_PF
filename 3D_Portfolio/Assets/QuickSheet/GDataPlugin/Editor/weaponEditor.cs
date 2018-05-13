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
[CustomEditor(typeof(weapon))]
public class weaponEditor : BaseGoogleEditor<weapon>
{	    
    public override bool Load()
    {        
        weapon targetData = target as weapon;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<weaponData>(targetData.WorksheetName) ?? db.CreateTable<weaponData>(targetData.WorksheetName);
        
        List<weaponData> myDataList = new List<weaponData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            weaponData data = new weaponData();
            
            data = Cloner.DeepCopy<weaponData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
