using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  // set in inspector
  // public SpawnableItem spawnableItems;
  public List<SpawnableItem> spawnableItems;
  public static GameManager Instance;
  // public static List<SpawnableItem> FindableItems { get; set; }
  public GameState State;
  public static GameStateData StateData { get; set; }
  public Actions GameActions;
  public static event Action<GameState> OnGameStateChanged;
  public static bool PostSetup { get; set; }

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    PostSetup = false;
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    Debug.Log(GameState.Setup);
    UpdateGameState(GameState.Setup);
    Debug.Log("game amanger start");
  }

  public void UpdateGameState(GameState newState, SpawnableItem item = null, List<SpawnableItem> items = null)
  {

    // Debug.Log($"new state: {newState}");
    // Debug.Log($"items {items}");
    State = newState;
    switch (newState)
    {
      case GameState.Setup:
        StateData = new GameStateData(GameActions.createFindableItems(), new List<string> { "clown", "star", "angel" });
        GameActions = new Actions();
        PostSetup = true;
        break;
      case GameState.PlayerCollectItem:
        // Debug.Log($"collected item: {item.ItemName}");
        StateData.AddCollectedItem(item);
        GameActions.CheckCollectedAllItems();
        break;
      case GameState.Win:
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnGameStateChanged?.Invoke(newState);
  }

  public class GameStateData
  {
    public static List<string> GoalItems { get; set; }
    public List<SpawnableItem> Collectables { get; set; }
    public static List<SpawnableItem> CollectedItems { get; set; }
    public List<Spawner> Spawners { get; set; }
    public List<GameObject> SpawnerObjects = GameObject.FindGameObjectsWithTag("ItemSpawner").ToList();


    public GameStateData(List<SpawnableItem> findableItems, List<string> goalItems)
    {
      Collectables = findableItems;
      GoalItems = goalItems;
      CollectedItems = new List<SpawnableItem>();
      Spawners = new List<Spawner>();
      foreach (var spawner in SpawnerObjects)
      {
        // Debug.Log($"spawner: {EditorJsonUtility.ToJson(spawner.GetComponent<ItemSpawner>().GetComponent<SpawnableItem>().ItemName)}");
        //               Spawner detailedSpawner = new Spawner(spawner.name, spawner.GetComponent<ItemSpawner>().foundObject.ItemName);
        //         Spawners.Add(detailedSpawner);
      }
      // foreach (var spawner in SpawnerObjects)
      // {
      //   // Debug.Log($"spawner: {EditorJsonUtility.ToJson(spawner)}");

      // }
    }



    public void AddCollectedItem(SpawnableItem item)
    {
      CollectedItems.Add(item);
    }


  }

  public class Actions
  {
    public void CheckCollectedAllItems()
    {
      bool Checker = false;
      foreach (var item in GameManager.GameStateData.CollectedItems)
      {
        if (!GameManager.GameStateData.GoalItems.Contains(item.ItemName))
        {
          Checker = true;
          break;
        }
      }
      if (Checker)
      {
        GameManager.Instance.UpdateGameState(GameState.Win);
      }
    }

    public List<SpawnableItem> createFindableItems()
    {
      List<SpawnableItem> findableItems = new List<SpawnableItem>();
      foreach (var item in GameManager.Instance.spawnableItems)
      {
        SpawnableItem detailedItem = new SpawnableItem(item.name, item);
        findableItems.Add(detailedItem);
      }
      return findableItems;
    }
  }


  public class Spawner
  {
    public string name { get; set; }
    public bool collected { get; set; }
    public string item { get; set; }

    public Spawner(string spawnerName, string itemName)
    {
      name = spawnerName;
      collected = false;
      item = itemName;
    }
  }
}

public enum GameState
{
  Setup,
  PlayerCollectItem,
  Win,
}
