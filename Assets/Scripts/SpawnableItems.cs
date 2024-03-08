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
  // GameObject clown = Resources.Load("Assets/Prefabs/clown.prefab") as GameObject;
  // GameObject star = Resources.Load("Assets/Prefabs/star.prefab") as GameObject;

  // Debug.Log(clown);
  // Debug.Log(star);
  // spawnableObjects.Add(clown);
  // spawnableObjects.Add(star);

}
