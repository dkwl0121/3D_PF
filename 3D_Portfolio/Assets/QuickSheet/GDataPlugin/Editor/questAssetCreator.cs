using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/quest")]
    public static void CreatequestAssetFile()
    {
        quest asset = CustomAssetUtility.CreateAsset<quest>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "quest";
        EditorUtility.SetDirty(asset);        
    }
    
}