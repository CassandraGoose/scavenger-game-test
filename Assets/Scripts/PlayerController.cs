using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  Rigidbody2D rigidbody2d;
  float horizontal;
  float vertical;
  List<SpawnableItem> collectedItems;

  public OutdoorDoorwayVectorValue startingPosition;
  // Start is called before the first frame update
  void Start()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
    transform.position = startingPosition.initialValue;
    collectedItems = new List<SpawnableItem>();
  }

  // Update is called once per frame
  void Update()
  {
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");
  }

  void FixedUpdate()
  {
    Vector2 position = rigidbody2d.position;
    position.x = position.x + 3.0f * horizontal * Time.deltaTime;
    position.y = position.y + 3.0f * vertical * Time.deltaTime;

    rigidbody2d.MovePosition(position);
  }

  public void CollectItem(SpawnableItem fish)
  {
    collectedItems.Add(fish);
    foreach(var item in collectedItems)
    {
      // Debug.Log(item.ItemName);
    }
  }
}

