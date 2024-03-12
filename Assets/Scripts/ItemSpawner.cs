using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSpawner : MonoBehaviour
{
  // set in inspector
  public List<GameObject> spawnableObjects;
  public GameObject uiBox;
  public Button acceptItemButton;


  public List<SpawnableItem> findableItems;
  public GameObject sparklePrefab;
  SpawnableItem foundObject;

  void Start()
  {
    createFindableItems();
    foundObject = GetRandomSpawnable();
    Instantiate(sparklePrefab, transform.position, Quaternion.identity);
    uiBox.SetActive(false);
    acceptItemButton.onClick.AddListener(triggerCollectItem);
  }

  void Update()
  {
    Collider2D spawnableCollider = gameObject.GetComponent<Collider2D>();
    Collider2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

    if (Input.GetButtonDown("Submit"))
    {
      if (Physics2D.Distance(spawnableCollider, player).distance <= 0)
      {
        DisplayUIBox();
      }
    }
  }

  public void DisplayUIBox()
  {
    uiBox.SetActive(true);
    Image foundItemUI = GameObject.Find("FoundItemUI").GetComponent<Image>();

    foundItemUI.sprite = foundObject.Prefab.GetComponent<SpriteRenderer>().sprite;

    GameObject foundItemText = GameObject.Find("FoundItemText");
    foundItemText.GetComponent<TextMeshProUGUI>().text = "Would you like to take this " + foundObject.ItemName + "?";
  }

  List<SpawnableItem> createFindableItems()
  {
    foreach (var item in spawnableObjects)
    {
      SpawnableItem detailedItem = new SpawnableItem(item.name, item);
      findableItems.Add(detailedItem);
    }
    return findableItems;
  }

  public SpawnableItem GetRandomSpawnable()
  {
    int random = Random.Range(0, spawnableObjects.Count - 1);
    return findableItems[random];
  }

  public void triggerCollectItem()
  {
    PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    player.CollectItem(foundObject);
    uiBox.SetActive(false);
    // all of this is for naught unless we tell unity to not erase and reset everything every time a new scene loads.
  }

}