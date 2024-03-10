using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnableItems : ScriptableObject
{
  public List<GameObject> spawnableObjects;

  public GameObject GetRandomSpawnable() {
    int random = Random.Range(0, spawnableObjects.Count - 1);
    return spawnableObjects[random];
  }
}
