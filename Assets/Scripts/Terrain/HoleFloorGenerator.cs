using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// responsible of creating a Holey floor ( floor with holes)

[DisallowMultipleComponent, ExecuteAlways]
public class HoleyFloorGenerator : MonoBehaviour
{
  [SerializeField] Transform floor;
  [SerializeField] TerrainGeneratorConfig config;

  void Start()
  {
    int holesAmount = GetSemiRandomHolesAmount(config.maxHolesAmount);
    List<int> holes = GenerateHoles(holesAmount);
    GenerateFloor(holes);
  }

  int GetSemiRandomHolesAmount(int max)
  {
    List<int> eventSpace = new List<int>();
    int likelyNess = 1;
    // we add the number of holes to the array 
    // such that 1 hole is twice as likely as 2 holes
    // so we add it twice more
    for (int i = max; i > 0; i--)
    {
      for (int j = 0; j < likelyNess; j++)
      {
        eventSpace.Add(i);
      }
      likelyNess = likelyNess * config.holeProbabilityFactor;
    }
    int randomIndex = Random.Range(0, eventSpace.Count);
    return eventSpace[randomIndex];
  }

  List<int> GenerateHoles(int amount)
  {
    List<int> holes = new List<int>();
    for (int i = 0; i < amount; i++)
    {
      holes.Add(Random.Range(config.minHoleWidth, config.maxHoleWidth + 1));
    }
    return holes;
  }

  void GenerateFloor(List<int> holes)
  {
    // we add floor tiles to a floor with holes next to then.
    // floor tiles are of random size.
    // we'd need to precompute holes if we want them random using this method
    int totalHolesWidth = holes.Sum();

    float remainingSpace = config.wallDistance - totalHolesWidth;
    float startX = config.GetLeftWallInnerSideX();
    for (int i = 0; i <= holes.Count; i++)
    {
      // so here we get a random width and then scale the floor tile with that value
      // and then position it relative to the startX pos
      // randomWidth is an int to have steps in the range, and not any value
      bool isLastTile = i == holes.Count;
      int tileWidth = isLastTile ? (int)remainingSpace
        : Random.Range(0, (int)remainingSpace + 1);

      if (tileWidth > 0)
        GenerateFloorTile(tileWidth, startX);

      // print($"startX: {startX}, spaceLeft: {remainingSpace}, floorScale.x: {tileWidth}");
      int nextHoleWidth = isLastTile ? 0 : holes[i];
      startX += tileWidth + nextHoleWidth;
      remainingSpace -= tileWidth;
    }
  }

  Transform GenerateFloorTile(int tileWidth, float spawnX)
  {
    Transform floorTile = InstantiateFloor();
    Vector3 tempScale = floorTile.localScale;
    Vector3 tempPosition = floorTile.position;
    floorTile.localScale = new Vector3(tileWidth, tempScale.y, tempScale.z);
    floorTile.position = new Vector3(
      // adding half the width because it's relative to center
      spawnX + (tileWidth / 2),
      spawnY,
      tempPosition.z
    );
    return floorTile;
  }

  void GenerateSideFloors(float spawnY)
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

  Transform InstantiateFloor()
  {
    Transform floorTile = Instantiate(floor);
    floorTile.SetParent(transform);
    return floorTile;
  }


}