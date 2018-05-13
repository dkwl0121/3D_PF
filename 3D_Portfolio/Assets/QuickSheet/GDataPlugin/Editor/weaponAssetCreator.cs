using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/weapon")]
    public static void CreateweaponAssetFile()
    {
        weapon asset = CustomAssetUtility.CreateAsset<weapon>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "weapon";
        EditorUtility.SetDirty(asset);        
    }
    
}