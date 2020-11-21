

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TerrainGeneratorConfig : ScriptableObject
{
  public Vector3 tileSize;
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
  public List<float> floorsY = new List<float>();
  public float leftWallX;
  public float rightWallX;
  public float leftWallInnerSideX;
  public float rightWallInnerSideX;

  public TerrainGeneratorConfig()
  {
    leftWallX = -wallDistance / 2;
    rightWallX = wallDistance / 2;
    leftWallInnerSideX = leftWallX + wallWidth / 2;
    rightWallInnerSideX = rightWallX - wallWidth / 2;
    ComputeFloorsY();
  }

  private void ComputeFloorsY()
  {
    float floorHeight = tileSize.y / floorsPerTile;
    float startTileY = tileSize.y / 2;
    float y = startTileY - floorHeight;
    for (int i = 0; i < floorsPerTile; i++)
    {
      floorsY.Add(y);
      y += floorHeight;
    }

  }

}