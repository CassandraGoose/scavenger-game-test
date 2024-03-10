using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
  SpriteRenderer sparkleSprite;
  GameObject sparkle;

  public void SetUp(GameObject sparkleObject)
  {
    sparkle = sparkleObject;
    sparkleSprite = sparkle.GetComponent<SpriteRenderer>();
    Hide();
  }

  void Update()
  {

    Collider2D sparkleCollider;
    Collider2D player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

  if (sparkle)
  {
    sparkleCollider = sparkle.GetComponent<Collider2D>();
    if (Input.GetButtonDown("Submit") && Physics2D.Distance(sparkleCollider, player).distance <= -1f)
    {
      Debug.Log("GET ITEM");
    }
  }
  }

  void OnTriggerStay2D(Collider2D player)
  {
    PlayerController playerController = player.GetComponent<PlayerController>();

    if (playerController != null)
    {
      StartCoroutine(FadeIn());
    }
  }

  private IEnumerator FadeIn()
  {
    if (sparkleSprite)
    {
      float alphaVal = sparkleSprite.color.a;
      Color tmp = sparkleSprite.color;

      while (sparkleSprite.color.a < 1)
      {
        alphaVal += 0.1f;
        tmp.a = alphaVal;
        sparkleSprite.color = tmp;

        yield return new WaitForSeconds(0.05f); // update interval
      }
    }
  }

  public void Hide()
  {
    Color tmp = sparkleSprite.color;
    tmp.a = 0f;
    sparkleSprite.color = tmp;
  }

}
