

using UnityEngine;
using System.Collections.Generic;


// holds the configuration of the Tiles and compute some basic information about them
[CreateAssetMenu(menuName = "ScriptableObjects/TileConfig")]
public class TileConfig : ScriptableObject
{
  public Vector3 tileSize = new Vector3(50, 40, 1);
  public Vector3 floorSize = new Vector3(1, 1, 3);
  public Vector3 wallSize = new Vector3(1, 1, 3);
  public int floorsPerTile = 3;
  // vertical walls distance
  public float wallDistance = 40f;
  public float wallWidth = 1f;
  public int minHoleWidth = 3;
  public int maxHoleWidth = 10;
  public int maxHolesAmount = 4;
  public int holeProbabilityFactor = 3;

  // computed
  // where to place floors on the y axis
  public List<float> floorsY;
  [HideInInspector] public float leftWallX;
  [HideInInspector] public float rightWallX;
  [HideInInspector] public float leftWallInnerSideX;
  [HideInInspector] public float rightWallInnerSideX;
  [HideInInspector] public float leftWallOuterSideX;
  [HideInInspector] public float rightWallOuterSideX;



  public TileConfig()
  {
    ComputeValues();
  }

  public void ComputeValues()
  {
    leftWallX = -wallDistance / 2 - wallWidth / 2;
    rightWallX = wallDistance / 2 + wallWidth / 2;
    leftWallInnerSideX = leftWallX + wallWidth / 2;
    rightWallInnerSideX = rightWallX - wallWidth / 2;
    leftWallOuterSideX = leftWallX - wallWidth / 2;
    rightWallOuterSideX = rightWallX + wallWidth / 2;
    ComputeFloorsY();
  }

  private void ComputeFloorsY()
  {
    floorsY = new List<float>();
    float floorHeight = tileSize.y / floorsPerTile;
    float startTileY = tileSize.y / 2 + floorSize.y / 2;
    float y = startTileY - floorHeight;
    for (int i = 0; i < floorsPerTile; i++)
    {
      floorsY.Add(y);
      y -= floorHeight;
    }
  }

}