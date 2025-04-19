using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float tileSize = 1f; // Size of each tile
    public int diceroll;
    private Vector2 targetPosition;
    private bool canMove = false;

    public Text dicerollText; // UI Text to display dice roll
    private Dice dice;
    private GridManager gridManager;

    void Start()
    {
        targetPosition = transform.position; // Initialize target position
        dice = GetComponent<Dice>(); // Get the Dice component
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Roll dice with Space key
        {
            RollDice();
        }

        if (canMove && Vector2.Distance(transform.position, targetPosition) < 0.01f) // Allow movement only when player reaches the target position
        {
            Vector2 direction = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.W)) direction = Vector2.up;    // Move up
            if (Input.GetKeyDown(KeyCode.S)) direction = Vector2.down;  // Move down
            if (Input.GetKeyDown(KeyCode.A)) direction = Vector2.left;  // Move left
            if (Input.GetKeyDown(KeyCode.D)) direction = Vector2.right; // Move right

            if (direction != Vector2.zero)
            {
                Vector2 newPosition = (Vector2)transform.position + direction * tileSize;

                Tile currentTile = gridManager.GetTileAtPosition(transform.position); // Get the current tile
                Tile targetTile = gridManager.GetTileAtPosition(newPosition); // Get the target tile

            if (currentTile != null && targetTile != null && currentTile.adjacentTiles.Contains(targetTile) && targetTile.isWalkable)
            {
                targetPosition = newPosition;
                diceroll--; // Decrease the dice roll count

             if (diceroll <= 0)
            {
                canMove = false; // Disable movement when dice roll is exhausted
                dicerollText.text = "Dice Roll: 0"; // Update the UI Text to indicate no more moves
            }
            }
            else
            {
                Debug.Log("Cannot move to the tile at " + newPosition); // Log if the tile is not walkable or not adjacent
            }
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f); // Move towards the target position
    }

    void RollDice()
    {
        diceroll = dice.RollDices(); // Roll the dice
        Debug.Log("Dice Roll: " + diceroll);
        dicerollText.text = "Dice Roll: " + diceroll; // Update the UI Text with the dice roll value
        canMove = true;
    }
}
