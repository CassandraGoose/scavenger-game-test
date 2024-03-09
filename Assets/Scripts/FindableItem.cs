using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindableItem : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

  void Awake()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Present()
    {
      rigidbody2d.AddForce(transform.up * 2f);
      
    }
}
