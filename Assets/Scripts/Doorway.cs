using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
  public string nextScene;
  public Vector2 playerPosition;
  public OutdoorDoorwayVectorValue PlayerStorage;

  public void OnTriggerEnter2D(Collider2D otherCollider) {
    Debug.Log("hi");
    if (otherCollider.CompareTag("Player") && !otherCollider.isTrigger) {
      Debug.Log(PlayerStorage);
      Debug.Log(playerPosition);
      PlayerStorage.initialValue = playerPosition;
      SceneManager.LoadScene(nextScene);
    }
  }
}