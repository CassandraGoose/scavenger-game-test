using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  public GameDataManager GameState;
  public static event Action<GameStateTrigger> OnGameStateChanged;


  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    SceneManager.sceneLoaded += OnSceneLoaded;
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    GameState = new GameDataManager();
    GameObject[] CollectablePrefabs = Resources.LoadAll<GameObject>("Prefabs/Fish");
    GameState.SetupCollectables(CollectablePrefabs);
    UpdateGameState(GameStateTrigger.Setup);
  }

  // ---------STATE UPDATES---------- //

  public void UpdateGameState(GameStateTrigger newState)
  {
    GameState.UpdateState(newState);

    switch (GameState.CurrentGameState)
    {
      case GameStateTrigger.Setup:
        break;
      case GameStateTrigger.PostSetup:
        break;
      case GameStateTrigger.PlayerCollectItem:
        GameState.CheckCollectionCompleted();
        break;
      case GameStateTrigger.ChangeScene:
        break;
      case GameStateTrigger.Win:
        // show closing UI.
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnGameStateChanged?.Invoke(newState);
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {

    Debug.Log($"{scene.name}");
    if (GameManager.Instance.GameState != null && GameManager.Instance.GameState.CurrentGameState != GameStateTrigger.Setup)
    {
      // Debug.Log("spawners found:");
      // foreach (KeyValuePair<string, ItemSpawnerData> pair in GameManager.Instance.GameState.spawners)
      // {
      //   Debug.Log($"{pair.Key}");
      // }
      if (scene.name == "SampleScene")
      {
        foreach (KeyValuePair<string, ItemSpawnerData> pair in GameManager.Instance.GameState.spawners)
        {

          // GameManager.ItemSpawnerData spawnerData = GameManager.Instance.GameState.FindItemSpawner(pair.Key);
          GameObject spawnerGameObject = GameObject.Find(pair.Key);
          // Debug.Log($"foudn spaner: {spawnerGameObject.name}");
          // Debug.Log($"data: {spawnerData.item.ItemName}");
          // ItemSpawner spawnerInstance = spawnerGameObject.AddComponent<ItemSpawner>();
          spawnerGameObject.AddComponent<ItemSpawner>();
          // spawnerInstance.ReloadData(spawnerData);
        }

      }
    }
  }

  // ---------HELPER CLASSES---------- //

  public class GameDataManager
  {
    public GameStateTrigger CurrentGameState { get; private set; }
    public List<SpawnableItem> AvailableCollectables { get; private set; }
    public List<string> GoalItems { get; private set; }
    public List<SpawnableItem> PlayerCollectedItems { get; private set; }
    public Dictionary<string, ItemSpawnerData> spawners { get; private set; }

    public GameDataManager()
    {
      this.GoalItems = new List<string> { "Star Fish", "Clown Fish", "Angel Fish" };
      this.PlayerCollectedItems = new List<SpawnableItem>();
      this.spawners = new Dictionary<string, ItemSpawnerData>();
      this.CurrentGameState = GameStateTrigger.Initialized;
    }

    public void UpdateState(GameStateTrigger newState)
    {
      this.CurrentGameState = newState;
    }

    // ---------ACTIONS---------- //

    public void SetupCollectables(GameObject[] CollectablePrefabs)
    {

      List<SpawnableItem> items = new List<SpawnableItem>();

      foreach (var prefab in CollectablePrefabs)
      {
        items.Add(new SpawnableItem(prefab.name, prefab));
      }
      this.AvailableCollectables = items;
    }

    public void CollectItem(ItemSpawnerData updatedData)
    {
      this.PlayerCollectedItems.Add(updatedData.item);
      this.spawners[updatedData.spawnerName].found = updatedData.found;
    }

    public ItemSpawnerData GetSpawnerData(GameObject spawner)
    {
      GameManager.ItemSpawnerData spawnerData = GameManager.Instance.GameState.FindItemSpawner(spawner.name);
      return spawnerData;
    }

    public void CheckCollectionCompleted()
    {
      bool allFound = AvailableCollectables.All(item => PlayerCollectedItems.Contains(item));
      if (allFound)
      {
        GameManager.Instance.UpdateGameState(GameStateTrigger.Win);
      }
    }

    public void AddItemSpawner(string spawnerName, SpawnableItem item, bool found)
    {
      this.spawners[spawnerName] = new ItemSpawnerData(spawnerName, item, found);
    }

    public ItemSpawnerData FindItemSpawner(string name)
    {
      ItemSpawnerData matchingSpawner = spawners[name];
      return matchingSpawner;
    }
  }

  public class ItemSpawnerData
  {
    public string spawnerName;
    public bool found;
    public SpawnableItem item;

    public ItemSpawnerData(string spawnerName, SpawnableItem item, bool found)
    {
      this.spawnerName = spawnerName;
      this.item = item;
      this.found = found;
    }
  }

}

// ---------ENUMS & INTERFACES ---------- //

public enum GameStateTrigger
{
  Initialized,
  Setup,
  PostSetup,
  PlayerCollectItem,
  ChangeScene,
  Win,
}




// public class GameManager : MonoBehaviour
// {
//   // set in inspector
//   // public SpawnableItem spawnableItems;
//   public List<SpawnableItem> spawnableItems;
//   public static GameManager Instance;
//   // public static List<SpawnableItem> FindableItems { get; set; }
//   public GameState State;
//   public static GameStateData StateData { get; set; }
//   public Actions GameActions;
//   public static event Action<GameState> OnGameStateChanged;
//   public static bool PostSetup { get; set; }

//   void Awake()
//   {
//     if (Instance != null)
//     {
//       Destroy(gameObject);
//       return;
//     }

//     Instance = this;
//     PostSetup = false;
//     DontDestroyOnLoad(gameObject);
//   }

//   void Start()
//   {
//     Debug.Log(GameState.Setup);
//     UpdateGameState(GameState.Setup);
//   }

//   public void UpdateGameState(GameState newState, SpawnableItem item = null, List<SpawnableItem> items = null)
//   {
//     State = newState;
//     switch (newState)
//     {
//       case GameState.Setup:
//         GameActions = new Actions();
//         StateData = new GameStateData(GameActions.createFindableItems(), new List<string> { "clown", "star", "angel" });
//         PostSetup = true;
//         break;
//       case GameState.PlayerCollectItem:
//         // Debug.Log($"collected item: {item.ItemName}");
//         StateData.AddCollectedItem(item);
//         GameActions.CheckCollectedAllItems();
//         break;
//       case GameState.Win:
//         break;
//       default:
//         throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
//     }

//     OnGameStateChanged?.Invoke(newState);
//   }

//   public class GameStateData
//   {
//     public static List<string> GoalItems { get; set; }
//     public List<SpawnableItem> Collectables { get; set; }
//     public static List<SpawnableItem> CollectedItems { get; set; }
//     public List<Spawner> Spawners { get; set; }
//     public List<GameObject> SpawnerObjects = GameObject.FindGameObjectsWithTag("ItemSpawner").ToList();


//     public GameStateData(List<SpawnableItem> findableItems, List<string> goalItems)
//     {
//       Debug.Log("in game state data");
//       Collectables = findableItems;
//       GoalItems = goalItems;
//       CollectedItems = new List<SpawnableItem>();
//       Spawners = new List<Spawner>();
//       Debug.Log($"collectables: {Collectables}, goal items: {goalItems}, collectedItems: {CollectedItems}, spanwers: {Spawners}");
//       foreach (var spawner in SpawnerObjects)
//       {
//         // Debug.Log($"spawner: {EditorJsonUtility.ToJson(spawner.GetComponent<ItemSpawner>().GetComponent<SpawnableItem>().ItemName)}");
//         //               Spawner detailedSpawner = new Spawner(spawner.name, spawner.GetComponent<ItemSpawner>().foundObject.ItemName);
//         //         Spawners.Add(detailedSpawner);
//       }
//       // foreach (var spawner in SpawnerObjects)
//       // {
//       //   // Debug.Log($"spawner: {EditorJsonUtility.ToJson(spawner)}");

//       // }
//     }



//     public void AddCollectedItem(SpawnableItem item)
//     {
//       CollectedItems.Add(item);
//     }


//   }

//   public class Actions
//   {
//     public void CheckCollectedAllItems()
//     {
//       bool Checker = false;
//       foreach (var item in GameManager.GameStateData.CollectedItems)
//       {
//         if (!GameManager.GameStateData.GoalItems.Contains(item.ItemName))
//         {
//           Checker = true;
//           break;
//         }
//       }
//       if (Checker)
//       {
//         GameManager.Instance.UpdateGameState(GameState.Win);
//       }
//     }

//     public List<SpawnableItem> createFindableItems()
//     {
//       Debug.Log("create findable items");
//       List<SpawnableItem> findableItems = new List<SpawnableItem>();
//       foreach (var item in GameManager.Instance.spawnableItems)
//       {
//         Debug.Log($"item: {item}");
//         SpawnableItem detailedItem = new SpawnableItem(item.name, item);
//         findableItems.Add(detailedItem);
//       }
//       return findableItems;
//     }
//   }


//   public class Spawner
//   {
//     public string name { get; set; }
//     public bool collected { get; set; }
//     public string item { get; set; }

//     public Spawner(string spawnerName, string itemName)
//     {
//       name = spawnerName;
//       collected = false;
//       item = itemName;
//     }
//   }
// }

// public enum GameStateTrigger
// {
//   Setup,
//   PlayerCollectItem,
//   Win,
// }
