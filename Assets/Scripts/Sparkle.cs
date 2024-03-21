using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
  SpriteRenderer sparkleSprite;

  void Start()
  {
    sparkleSprite = gameObject.GetComponent<SpriteRenderer>();
    Hide();
  }

  void OnTriggerStay2D(Collider2D player)
  {
    PlayerController playerController = player.GetComponent<PlayerController>();
    if (playerController != null)
    {
      StartCoroutine(FadeIn());
    }
  }

  public IEnumerator FadeIn()
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

  public void DestroySparkle()
  {
    Destroy(gameObject);
  }
}
