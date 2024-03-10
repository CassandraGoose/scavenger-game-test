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
    if (otherCollider.CompareTag("Player") && !otherCollider.isTrigger) {
      PlayerStorage.initialValue = playerPosition;
      SceneManager.LoadScene(nextScene);
    }
  }
}
