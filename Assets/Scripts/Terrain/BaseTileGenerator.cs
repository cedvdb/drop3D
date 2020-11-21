using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// responsible of creating a tile of given size with its side walls

[DisallowMultipleComponent, ExecuteAlways]
public class BaseTileGenerator : MonoBehaviour
{
  [SerializeField] Transform background;
  [SerializeField] Transform wall;
  [SerializeField] Transform floor;
  [SerializeField] TerrainGeneratorConfig config;

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
    Vector3 leftPos = leftWall.position;
    Vector3 rightPos = rightWall.position;
    leftPos.x = config.leftWallX;
    rightPos.x = config.rightWallX;
    leftWall.position = leftPos;
    rightWall.position = rightPos;
  }


  void GenerateSideFloors()
  {
    foreach (float y in config.floorsY)
    {
      Transform leftTile = Instantiate(floor, transform);
      Transform rightTile = Instantiate(floor, transform);
      Vector3 leftPosition = leftTile.position;
      Vector3 rightPosition = rightTile.position;
      leftPosition.x = leftWallSideX - (leftTile.localScale.x / 2);
      rightPosition.x = rightWallSideX + (rightTile.localScale.x / 2);
      leftPosition.y = spawnY;
      rightPosition.y = spawnY;
      leftTile.position = leftPosition;
      rightTile.position = rightPosition;
    }
  }

  void GenerateSideFloors()
  {
    Transform leftTile = InstantiateFloor();
    Transform rightTile = InstantiateFloor();
    Vector3 leftPosition = leftTile.position;
    Vector3 rightPosition = rightTile.position;
    float leftWallSideX = wallLeft.position.x - wallLeft.localScale.x / 2;
    float rightWallSideX = wallRight.position.x + wallRight.localScale.x / 2;
    leftPosition.x = leftWallSideX - (leftTile.localScale.x / 2);
    rightPosition.x = rightWallSideX + (rightTile.localScale.x / 2);
    leftPosition.y = spawnY;
    rightPosition.y = spawnY;
    leftTile.position = leftPosition;
    rightTile.position = rightPosition;
  }

}