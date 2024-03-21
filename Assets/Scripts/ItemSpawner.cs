using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Linq;
// using UnityEngine.SceneManagement;

public class ItemSpawner : MonoBehaviour
{
  GameObject Sparkles;
  SpawnableItem item;
  bool inCollision;
  bool found;
  public GameObject uiBox;

  // dev must assign Canvas->Image->Button to acceptItemButton in each ItemSpawner.
  public Button acceptItemButton;


  void Awake()
  {
    GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

    if (GameManager.Instance != null)
    {
      if (GameManager.Instance.GameState != null)
      {
        if (GameManager.Instance.GameState.CurrentGameState != GameStateTrigger.Setup)
        {
          ReloadData(GameManager.Instance.GameState.GetSpawnerData(gameObject));
        }
      }
    }
  }

  void GameManager_OnGameStateChanged(GameStateTrigger newState)
  {
    switch (newState)
    {
      case GameStateTrigger.Setup:
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        uiBox = child;
        SetupSparkles();
        item = GetRandomSpawnable(GameManager.Instance.GameState.AvailableCollectables);
        found = false;
        uiBox.SetActive(false);
        GameManager.Instance.GameState.AddItemSpawner(gameObject.name, item, found);
        GameManager.Instance.GameState.UpdateState(GameStateTrigger.PostSetup);

        break;
    }
  }

  void Start()
  {
    if (acceptItemButton == null)
    {
      acceptItemButton = uiBox.GetComponentsInChildren<Button>(true)[0];
    }
    acceptItemButton.onClick.AddListener(() => triggerCollectItem());

  }

  void SetupSparkles()
  {
    GameObject SparklesPrefab = Resources.Load<GameObject>("Prefabs/Sparkles/Sparkles");
    Sparkles = Instantiate(SparklesPrefab, transform.position, Quaternion.identity);
  }

  void Update()
  {
    if (Input.GetButtonDown("Submit") && inCollision && !found)
    {

      DisplayUIBox();
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {

    if (collision.gameObject.tag == "Player")
    {
      inCollision = true;
    }
  }

  public void ReloadData(GameManager.ItemSpawnerData data)
  {
    GameObject child = gameObject.transform.GetChild(0).gameObject;
    uiBox = child;
    found = data.found;
    if (!found)
    {
      SetupSparkles();
      // Debug.Log($"reloaded data: {Sparkles.name}");
    }
    uiBox.SetActive(false);
    item = data.item;

    // Debug.Log($"reloaded data: {found}");

    Debug.Log($"reloaded data:  {item.PrefabName}");

    // Debug.Log($"reloaded data:  {uiBox.name}");
  }

  public SpawnableItem GetRandomSpawnable(List<SpawnableItem> findableItems)
  {
    int random = Random.Range(0, findableItems.Count);
    return findableItems.ElementAt(random);
  }

  void DisplayUIBox()
  {

      uiBox.SetActive(true);

      Image foundItemUI = GameObject.Find("FoundItemUI").GetComponent<Image>();
      Debug.Log($"item: {item.PrefabName}");
      string thing = $"Prefabs/Fish/{item.PrefabName}";
      Debug.Log($"fjdls:JJIII: {Resources.Load<GameObject>(thing).name}");
      foundItemUI.sprite = Resources.Load<GameObject>(thing).GetComponent<SpriteRenderer>().sprite;
I 
      GameObject foundItemText = GameObject.Find("FoundItemText");
      foundItemText.GetComponent<TextMeshProUGUI>().text = "Would you like to take this " + item.ItemName + "?";

  }

  void triggerCollectItem()
  {
    GameManager.Instance.UpdateGameState(GameStateTrigger.PlayerCollectItem);
    inCollision = false;
    Sparkles.SetActive(false);
    found = true;
    uiBox.SetActive(false);
    GameManager.ItemSpawnerData updatedData = new GameManager.ItemSpawnerData(gameObject.name, item, found);
    GameManager.Instance.GameState.CollectItem(updatedData);
  }
}