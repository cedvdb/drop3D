using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/**
  Generates obstacles on a tile
*/
public class ObstacleGenerator : MonoBehaviour
{

  [Header("Prefabs")]
  public Transform floor;

  [Header("Floors")]
  public int floorsPerTile = 4;

  [Header("Holes")]
  public int minHoleWidth = 3;
  public int maxHoleWidth = 10;
  public int maxHolesAmount = 4;
  public int holeProbabilityFactor = 3;

  private Transform background;
  // side walls
  private Transform wallLeft;
  private Transform wallRight;
  private float wallDistance;
  private List<Transform> obstacles;

  public static float tileHeight;

  void Awake()
  {
    Assert.IsTrue(transform.childCount == 3);
    FindChildren();
    ComputeDistances();
  }

  void FindChildren()
  {
    background = transform.Find("Background");
    wallLeft = transform.Find("Wall Left");
    wallRight = transform.Find("Wall Right");
  }

  void ComputeDistances()
  {
    MeshRenderer bgMesh = background.GetComponent<MeshRenderer>();
    Vector3 tileSize = bgMesh.bounds.size;
    tileHeight = tileSize.y;
    wallDistance = Vector3.Distance(
      wallLeft.position,
      wallRight.position
    ) - (wallLeft.localScale.x);
  }

  void Start()
  {
    GenerateTileFloors();
  }

  void GenerateTileFloors()
  {
    float floorHeight = tileHeight / floorsPerTile;
    // the origin is in the middle of the rectangle,
    // therefor we want to start spawning floors at the beginning + floor height
    float spawnY = transform.position.y + tileHeight / 2 - floorHeight;
    Vector3 tilePosition = transform.position;
    for (int i = 0; i < floorsPerTile; i++)
    {
      GenerateSideFloors(spawnY);
      int holesAmount = GetSemiRandomHolesAmount(maxHolesAmount);
      GenerateHoleyFloor(spawnY, holesAmount);
      spawnY -= floorHeight;
    }
  }

  void GenerateHoleyFloor(float spawnY, int holesAmount)
  {
    // we add floor tiles to a floor with holes next to then.
    // floor tiles are of random size.
    // we'd need to precompute holes if we want them random using this method
    List<int> holes = GenerateHoles(holesAmount);
    int totalHolesWidth = holes.Sum();

    float remainingSpace = wallDistance - totalHolesWidth;
    float startX = wallLeft.position.x + wallLeft.localScale.x;
    for (int i = 0; i <= holesAmount; i++)
    {
      // so here we get a random width and then scale the floor tile with that value
      // and then position it relative to the startX pos
      // randomWidth is an int to have steps in the range, and not any value
      bool isLastTile = i == holesAmount;
      int tileWidth = isLastTile ? (int)remainingSpace
        : Random.Range(0, (int)remainingSpace + 1);

      if (tileWidth > 0)
        GenerateFloorTile(tileWidth, startX, spawnY);

      // print($"startX: {startX}, spaceLeft: {remainingSpace}, floorScale.x: {tileWidth}");
      int nextHoleWidth = isLastTile ? 0 : holes[i];
      startX += tileWidth + nextHoleWidth;
      remainingSpace -= tileWidth;
    }
  }

  List<int> GenerateHoles(int amount)
  {
    List<int> holes = new List<int>();
    for (int i = 0; i < amount; i++)
    {
      holes.Add(Random.Range(minHoleWidth, maxHoleWidth + 1));
    }
    return holes;
  }

  Transform GenerateFloorTile(int tileWidth, float spawnX, float spawnY)
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
      likelyNess = likelyNess * holeProbabilityFactor;
    }
    int randomIndex = Random.Range(0, eventSpace.Count);
    return eventSpace[randomIndex];
  }

  Transform InstantiateFloor()
  {
    Transform floorTile = Instantiate(floor);
    floorTile.SetParent(transform);
    return floorTile;
  }

}
