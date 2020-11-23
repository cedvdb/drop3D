using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Tile : MonoBehaviour
{
  [SerializeField] Transform baseTile;
  [SerializeField] Transform holeyFloor;
  [SerializeField] TileConfig config;

  void Start()
  {
    Instantiate(baseTile, transform);
    PutHoleyFLoors();
  }

  void PutHoleyFLoors()
  {
    foreach (float y in config.floorsY)
    {
      print(y);
      Instantiate(
        holeyFloor,
        new Vector3(0, y + transform.position.y, 0),
        Quaternion.identity,
        transform
      );
    }
  }
}
