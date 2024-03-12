using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableItem : MonoBehaviour
{
  public string ItemName { get; set; }
  public GameObject Prefab { get; set; }
  public string Rarity { get; set; }
  Rigidbody2D rigidbody2d;

  public SpawnableItem(string name, GameObject prefab)
  {
    ItemName = name;
    Prefab = prefab;
    Rarity = getRarity(name);
  }

  void Awake()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  void Start()
  {
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
