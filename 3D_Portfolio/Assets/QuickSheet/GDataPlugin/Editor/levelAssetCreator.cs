using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/level")]
    public static void CreatelevelAssetFile()
    {
        level asset = CustomAssetUtility.CreateAsset<level>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "level";
        EditorUtility.SetDirty(asset);        
    }
    
}