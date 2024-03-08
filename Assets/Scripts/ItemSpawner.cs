using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  public SpawnableItems spawnableObjects;
  public GameObject sparklePrefab;

  // Start is called before the first frame update
  void Start()
  {
    GameObject data = spawnableObjects.GetRandomSpawnable();
    // Debug.Log(JsonUtility.ToJson(spawnableObjects, true));
    // GameObject randomItem = 
    // int random = Random.Range(0, spawnableObjects.Count - 1);
    // item will be shown when player interacts with sparkle. 
    // item is at spawnableObjects[random]
    Debug.Log(data);
    Instantiate(sparklePrefab, transform.position, Quaternion.identity);
  }
}
