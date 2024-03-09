using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public SpawnableItems spawnableObjects;
  public GameObject sparklePrefab;
  GameObject foundObject;
  // Start is called before the first frame update
  void Start()
  {
    foundObject = spawnableObjects.GetRandomSpawnable();
    GameObject sparkleObject = Instantiate(sparklePrefab, transform.position, Quaternion.identity);
    Sparkle sparkle = sparkleObject.AddComponent<Sparkle>();
    sparkle.SetUp(sparkleObject);
  }

  void showFound()
  {
    FindableItem foundItem = Instantiate(foundObject, transform.position, Quaternion.identity).GetComponent<FindableItem>();
    foundItem.Present();
  }

}
