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
[CustomEditor(typeof(enemy))]
public class enemyEditor : BaseGoogleEditor<enemy>
{	    
    public override bool Load()
    {        
        enemy targetData = target as enemy;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<enemyData>(targetData.WorksheetName) ?? db.CreateTable<enemyData>(targetData.WorksheetName);
        
        List<enemyData> myDataList = new List<enemyData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            enemyData data = new enemyData();
            
            data = Cloner.DeepCopy<enemyData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
