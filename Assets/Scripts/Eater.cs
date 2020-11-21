using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
  float speed = 5;

  void Update()
  {
    transform.Translate(Vector3.down * Time.deltaTime * speed);
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      SceneLoader.ShowHomeUI();
    }
  }
}
