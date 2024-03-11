using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSpawner : MonoBehaviour
{
  public SpawnableItems spawnableObjects;
  public GameObject sparklePrefab;
  Sparkle sparkle;
  GameObject foundObject;
  public GameObject uiBox;
  public GameObject foundItem;
  public Button acceptItemButton;

  // Start is called before the first frame update
  void Start()
  {
    foundObject = spawnableObjects.GetRandomSpawnable();
    GameObject sparkleObject = Instantiate(sparklePrefab, transform.position, Quaternion.identity);
    sparkle = sparkleObject.AddComponent<Sparkle>();
    sparkle.SetUp(sparkleObject);
    uiBox.SetActive(false);
    ignoreSparkleCollision();
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

  void ignoreSparkleCollision()
  {
    Collider2D sparkleCollider = sparkle.GetComponent<Collider2D>();
    Physics2D.IgnoreCollision(sparkleCollider, gameObject.GetComponent<Collider2D>());
  }


  public void DisplayUIBox()
  {
    uiBox.SetActive(true);
    Sprite foundObjectSprite = foundObject.GetComponent<SpriteRenderer>().sprite;
    foundItem = GameObject.Find("FoundItem");
    Image foundItemImage = foundItem.GetComponent<Image>();

    foundItemImage.sprite = foundObjectSprite;

    GameObject foundItemText = GameObject.Find("FoundItemText");
    foundItemText.GetComponent<TextMeshProUGUI>().text = "Would you like to take this " + foundObject.name + "?";
  }

  public void triggerCollectItem()
  {
    PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    player.CollectItem(foundItem);
    uiBox.SetActive(false);
    sparkle.DestroySparkle();
  }

}
