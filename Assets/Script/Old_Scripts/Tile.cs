using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isWalkable = true; // Flag to indicate if the tile is walkable

    public enum TileType
    {
        Normal,
        Dangerous,
        Beneficial
    }
    public TileType tileType = TileType.Normal; // Type of the tile

    public List<Tile> adjacentTiles = new List<Tile>(); // List of adjacent tiles
}
