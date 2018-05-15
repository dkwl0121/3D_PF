using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/item")]
    public static void CreateitemAssetFile()
    {
        item asset = CustomAssetUtility.CreateAsset<item>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "item";
        EditorUtility.SetDirty(asset);        
    }
    
}