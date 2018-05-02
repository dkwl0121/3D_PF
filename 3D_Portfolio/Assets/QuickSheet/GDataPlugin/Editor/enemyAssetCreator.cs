using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/enemy")]
    public static void CreateenemyAssetFile()
    {
        enemy asset = CustomAssetUtility.CreateAsset<enemy>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "enemy";
        EditorUtility.SetDirty(asset);        
    }
    
}