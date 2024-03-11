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

  void OnTriggerStay2D(Collider2D player)
  {
    // this probably would have been better as a circle cast!!! then, use onTriggerStay2d with a small collider to do the above 'get item' stuff!!!
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
