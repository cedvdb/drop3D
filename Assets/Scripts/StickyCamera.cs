using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StickyCamera : MonoBehaviour
{
  private Transform lookAt;
  private Vector3 cameraOffset;
  // Start is called before the first frame update
  void Start()
  {
    lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    cameraOffset = lookAt.position - transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    // we only follow the object on the y axis
    transform.position = new Vector3(
      transform.position.x,
      lookAt.position.y - cameraOffset.y,
      transform.position.z
    );
  }
}
