using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ItemSpawner : MonoBehaviour
{
  // set in inspector
  public GameObject uiBox;
  public Button acceptItemButton;

  public GameObject sparklePrefab;
  public SpawnableItem foundObject;
  GameObject sparkle;
  bool inCollision;

  void Awake()
  {
    GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
  }

  void Start()
  {

// FOR LOADING AFTER SCENE CHANGES: 
      // GameManager.Spawner currentSpawner = GameManager.StateData.Spawners.Find(spawner => spawner.name == gameObject.name);
      // string foundObjectName = currentSpawner.item;
      // foundObject = GameManager.StateData.Collectables.Find(item => item.ItemName == foundObjectName);
    //   if (!currentSpawner.collected)
    //   {
    //     sparkle = Instantiate(sparklePrefab, transform.position, Quaternion.identity);
    //   }
    // uiBox.SetActive(false);
  }

  void Update()
  {
    if (Input.GetButtonDown("Submit") && inCollision)
    {
      DisplayUIBox(foundObject);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // Debug.Log($"Current game object: {gameObject.name}");
    // Debug.Log($"Collision with {collision.gameObject.tag}");
    if (collision.gameObject.tag == "Player")
    {
      inCollision = true;
    }
  }

  void OnCollisionExit2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      inCollision = false;
    }
  }

  void OnDestroy()
  {
    GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
  }

  void GameManager_OnGameStateChanged(GameState newState)
  {
    if (newState == GameState.Setup)
    {
      foundObject = GetRandomSpawnable(GameManager.StateData.Collectables);
      sparkle = Instantiate(sparklePrefab, transform.position, Quaternion.identity);
      uiBox.SetActive(false);

    }

  }

  public void DisplayUIBox(SpawnableItem foundObject)
  {
    uiBox.SetActive(true);
    Image foundItemUI = GameObject.Find("FoundItemUI").GetComponent<Image>();

    foundItemUI.sprite = foundObject.Prefab.GetComponent<SpriteRenderer>().sprite;

    GameObject foundItemText = GameObject.Find("FoundItemText");
    foundItemText.GetComponent<TextMeshProUGUI>().text = "Would you like to take this " + foundObject.ItemName + "?";


    acceptItemButton.onClick.RemoveAllListeners();
    acceptItemButton.onClick.AddListener(() => triggerCollectItem(foundObject));
  }



  public SpawnableItem GetRandomSpawnable(List<SpawnableItem> findableItems)
  {
    int random = Random.Range(0, GameManager.StateData.Collectables.Count - 1);
    Debug.Log($"random: {random}");
    return findableItems[random];
  }

  public void triggerCollectItem(SpawnableItem foundObject)
  {
    // Debug.Log($"found object: {foundObject.ItemName}");
    GameManager.Instance.UpdateGameState(GameState.PlayerCollectItem, foundObject);
    uiBox.SetActive(false);
    inCollision = false;
    sparkle.GetComponent<Sparkle>().DestroySparkle();
    Destroy(foundObject);
    // all of this is for naught unless we tell unity to not erase and reset everything every time a new scene loads.
  }

}