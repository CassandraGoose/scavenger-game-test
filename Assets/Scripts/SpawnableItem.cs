using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnableItem : MonoBehaviour
{
  public string ItemName { get; set; }
  public GameObject Prefab { get; set; }
  public string PrefabName { get; set; }
  public string Rarity { get; set; }
  Rigidbody2D rigidbody2d;

  public SpawnableItem(string name, GameObject prefab)
  {
    ItemName = name;
    PrefabName = prefab.name;
    Rarity = getRarity(name);
  }


  void Start()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
    if (gameObject.transform.parent == gameObject.GetComponentsInParent<Transform>()[1]) {
       //Do stuff here
      //  Debug.Log(gameObject.GetComponentsInParent<Transform>()[1]);
    }
  }

  string getRarity(string item)
  {
    switch (item)
    {
      case "cod":
        return "common";
      case "star":
        return "uncommon";
      case "axlotl":
        return "rare";
      case "angel":
        return "legendary";
      default:
        return "common";
    }
  }

  public override string ToString()
  {
    return EditorJsonUtility.ToJson(this);
  }
}
