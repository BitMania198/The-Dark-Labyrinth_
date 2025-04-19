using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile[,] grid; // 2D array of tiles
    public int gridWidth;
    public int gridHeight;
    public float tileSize;

    public GameObject tilePrefab; // Prefab for creating tiles

    void Start()
    {
        InitializeGrid();
        AssignAdjacentTiles();
    }

    void InitializeGrid()
    {
                // Initialize the grid array
        grid = new Tile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Instantiate a tile at the correct position
                Vector3 position = new Vector3(x * tileSize, y * tileSize, 0);
                GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity);

                // Get the Tile component and assign it to the grid
                Tile tile = tileObject.GetComponent<Tile>();
                if (tile != null)
                {
                    grid[x, y] = tile;
                }
                else
                {
                    Debug.LogError("Tile prefab is missing the Tile component!");
                }
            }
        }
    }

    void AssignAdjacentTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Tile currentTile = grid[x, y];
                if (currentTile != null)
                {
                    List<Tile> neighbors = new List<Tile>();

                    // Check adjacent positions (up, down, left, right)
                    if (x > 0) neighbors.Add(grid[x - 1, y]); // Left
                    if (x < gridWidth - 1) neighbors.Add(grid[x + 1, y]); // Right
                    if (y > 0) neighbors.Add(grid[x, y - 1]); // Down
                    if (y < gridHeight - 1) neighbors.Add(grid[x, y + 1]); // Up

                    // Filter out null tiles and assign to adjacentTiles
                    currentTile.adjacentTiles = neighbors.FindAll(tile => tile != null);
                }
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize);
        int y = Mathf.RoundToInt(position.y / tileSize);

        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return grid[x, y];
        }
        return null;
    }
}
