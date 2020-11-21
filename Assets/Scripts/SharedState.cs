

using UnityEngine;

class TerrainGeneratorConfig : ScriptableObject
{
  public Vector3 tileSize;
  public int floorsPerTile = 3;
  public float wallDistance = 40;
  public int minHoleWidth = 3;
  public int maxHoleWidth = 10;
  public int maxHolesAmount = 4;
  public int holeProbabilityFactor = 3;

}