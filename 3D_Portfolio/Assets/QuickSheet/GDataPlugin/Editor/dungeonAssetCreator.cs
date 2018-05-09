using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/dungeon")]
    public static void CreatedungeonAssetFile()
    {
        dungeon asset = CustomAssetUtility.CreateAsset<dungeon>();
        asset.SheetName = "3D_PF_DB";
        asset.WorksheetName = "dungeon";
        EditorUtility.SetDirty(asset);        
    }
    
}