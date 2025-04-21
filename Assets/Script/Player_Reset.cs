using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Reset : MonoBehaviour
{
   public Transform startTransform; // Reference to the starting position Transform
    public Player_Movement_TileSize playerMovement; // Reference to the Player_Movement_TileSize script

    private void Awake()
    {
        // Get the Player_Movement_TileSize component
        playerMovement = GetComponent<Player_Movement_TileSize>();
    }

    // Method to reset the player to the starting position
    public void ResetToStart()
    {
        if (startTransform != null)
        {
            // Set the player's position to the position of the startTransform
            transform.position = startTransform.position;

            // Reset the movePoint position to the startTransform
            if (playerMovement != null && playerMovement.movePoint != null)
            {
                playerMovement.movePoint.position = startTransform.position;
            }

            // Stop the player's movement
            Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero; // Stop all movement
                playerRigidbody.angularVelocity = 0f;   // Stop any rotation
            }

            // Disable movement
            if (playerMovement != null)
            {
                playerMovement.canMove = false; // Prevent the player from moving
                Debug.Log("Player movement disabled after reset.");
            }
        }
        else
        {
            Debug.LogWarning("Start Transform is not assigned!");
        }
    }
}
