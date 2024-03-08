using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
  List<GameObject> spawnableObjects;
  public GameObject sparklePrefab;

  // Start is called before the first frame update
  void Start()
  {
    GameObject clown = Resources.Load("Assets/Prefabs/clown") as GameObject;
    GameObject star = Resources.Load("Assets/Prefabs/star") as GameObject;

    spawnableObjects.Add(clown);
    spawnableObjects.Add(star);
    Debug.Log(spawnableObjects.Count);
    // int random = Random.Range(0, spawnableObjects.Count - 1);
    // item will be shown when player interacts with sparkle. 
    // item is at spawnableObjects[random]
    Instantiate(sparklePrefab, transform.position, Quaternion.identity);
  }
}
