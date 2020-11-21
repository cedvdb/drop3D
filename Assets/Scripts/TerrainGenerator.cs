
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class TerrainGenerator : MonoBehaviour
{
  // tile to be spawned
  public GameObject tile;
  private Transform playerTranform;
  // where the next tile spawnz on the y axis
  private float nextTileSpawnY = 0f;
  // how many tiles we want
  private int amountTilesOnScreen = 3;
  private float tileHeight;

  void Start()
  {
    playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    AddInitialTiles();
    tileHeight = ObstacleGenerator.tileHeight;
  }

  // Update is called once per frame
  void Update()
  {
    // if the player reach the middle of all generated tiles
    // we spawn the next tile
    float playerY = playerTranform.transform.position.y;
    float halfAllTiles = tileHeight * amountTilesOnScreen / 2;
    if (playerY < nextTileSpawnY + halfAllTiles)
    {
      AddNextTile();
    }
  }

  void AddInitialTiles()
  {
    for (int i = 0; i < amountTilesOnScreen; i++)
    {
      AddNextTile();
    }
  }

  void AddNextTile()
  {
    // using Vector3.up because nextTileSpawnY is negative
    Vector3 pos = Vector3.up * nextTileSpawnY;
    GameObject tileInstance = Instantiate(tile, pos, Quaternion.identity, transform);
    nextTileSpawnY -= ObstacleGenerator.tileHeight;
    if (transform.childCount > amountTilesOnScreen)
    {
      Destroy(transform.GetChild(0).gameObject);
    }
  }
}
