using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  // set in inspector
  public SpawnableItem spawnableItems;

  public static GameManager Instance;
  public GameState State;
  public static List<SpawnableItem> FindableItems { get; set; }
  public static event Action<GameState> OnGameStateChanged;

  void Awake()
  {
    Instance = this;
  }
  // Start is called before the first frame update
  void Start()
  {
    UpdateGameState(GameState.Setup);
  }

  public void UpdateGameState(GameState newState)
  {
    State = newState;
    switch (newState)
    {
      case GameState.Setup:
        break;
      case GameState.Instructions:
        break;
      case GameState.PlayerSearch:
        break;
      case GameState.PlayerCollectItem:
        break;
      case GameState.Win:
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnGameStateChanged?.Invoke(newState);
  }


  public enum GameState
  {
    Setup,
    Instructions,
    PlayerSearch,
    PlayerCollectItem,
    Win,
  }

}
