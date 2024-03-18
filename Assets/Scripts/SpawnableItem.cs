using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableItem : MonoBehaviour
{
  public string ItemName { get; set; }
  public SpawnableItem Prefab { get; set; }
  public string Rarity { get; set; }
  Rigidbody2D rigidbody2d;

  public SpawnableItem(string name, SpawnableItem prefab)
  {
    ItemName = name;
    Prefab = prefab;
    Rarity = getRarity(name);
  }


  void Start()
  {
    Debug.Log("SpawnableItem Start");
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
}
