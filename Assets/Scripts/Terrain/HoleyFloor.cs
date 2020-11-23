using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// responsible of creating a Holey floor ( floor with holes)

[DisallowMultipleComponent]
public class HoleyFloor : MonoBehaviour
{
  [SerializeField] Transform floor;
  [SerializeField] TileConfig config;

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
    float startX = config.leftWallInnerSideX;
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
    Transform floorTile = Instantiate(floor, transform);
    Vector3 tempScale = floorTile.localScale;
    Vector3 tempPosition = floorTile.localPosition;
    floorTile.localScale = new Vector3(tileWidth, tempScale.y, tempScale.z);
    floorTile.localPosition = new Vector3(
      // adding half the width because it's relative to center
      spawnX + (tileWidth / 2),
      tempPosition.y,
      tempPosition.z
    );
    return floorTile;
  }

}