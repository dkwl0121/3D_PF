using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class weaponData
{
  [SerializeField]
  int no;
  public int No { get {return no; } set { no = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { name = value;} }
  
  [SerializeField]
  string description;
  public string Description { get {return description; } set { description = value;} }
  
  [SerializeField]
  int price;
  public int Price { get {return price; } set { price = value;} }
  
  [SerializeField]
  float att;
  public float Att { get {return att; } set { att = value;} }
  
  [SerializeField]
  float inte;
  public float Inte { get {return inte; } set { inte = value;} }
  
  [SerializeField]
  string iconloadpath;
  public string Iconloadpath { get {return iconloadpath; } set { iconloadpath = value;} }
  
  [SerializeField]
  string weaponloadpath;
  public string Weaponloadpath { get {return weaponloadpath; } set { weaponloadpath = value;} }
  
}