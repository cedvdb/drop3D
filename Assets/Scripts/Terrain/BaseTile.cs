using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// responsible of creating a tile of given size with its side walls

[DisallowMultipleComponent]
public class BaseTile : MonoBehaviour
{
  [SerializeField] Transform background;
  [SerializeField] Transform wall;
  [SerializeField] Transform floor;
  [SerializeField] TileConfig config;

  void Start()
  {
    GenerateBackground();
    GenerateWalls();
    GenerateSideFloors();
  }

  void GenerateBackground()
  {
    Transform bg = Instantiate(background, transform);
    bg.transform.localScale = config.tileSize;
  }

  void GenerateWalls()
  {
    Transform leftWall = Instantiate(wall, transform);
    Transform rightWall = Instantiate(wall, transform);
    Vector3 leftPos = leftWall.localPosition;
    Vector3 rightPos = rightWall.localPosition;
    leftPos.x = config.leftWallX;
    rightPos.x = config.rightWallX;
    leftWall.localPosition = leftPos;
    rightWall.localPosition = rightPos;
    Vector3 scale = leftWall.localScale;
    scale.y = config.tileSize.y;
    leftWall.localScale = scale;
    rightWall.localScale = scale;
  }


  void GenerateSideFloors()
  {
    print("generating side floors");
    foreach (float y in config.floorsY)
    {
      Transform leftTile = Instantiate(floor, transform);
      Transform rightTile = Instantiate(floor, transform);
      leftTile.name = "Left Side Floor";
      rightTile.name = "Right Side Floor";
      Vector3 leftPosition = leftTile.localPosition;
      Vector3 rightPosition = rightTile.localPosition;
      print(leftPosition.y);
      leftPosition.x = config.leftWallOuterSideX - (leftTile.localScale.x / 2);
      rightPosition.x = config.rightWallOuterSideX + (rightTile.localScale.x / 2);
      leftPosition.y = y;
      rightPosition.y = y;
      leftTile.localPosition = leftPosition;
      rightTile.localPosition = rightPosition;
    }
  }

}