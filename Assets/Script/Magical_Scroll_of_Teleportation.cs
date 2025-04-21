using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magical_Scroll_of_Teleportation : MonoBehaviour
{
    public bool choiceEvent = false; // Flag to indicate if the choice event is active

    public bool rollEvent = false; // Flag to indicate if the roll event is active

    
    public int maxSquaresToMove = 6;

    public Player_Movement_TileSize playerMovement; // Reference to the Player_Movement_TileSize script

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Player")) // Check if the collider belongs to the player
        {
            UseScroll(); // Call the method to use the scroll
        }
        else
        {
            Debug.LogWarning("Collider does not belong to the player!");
        }
    }

    public void UseScroll()
    {
        MoveUpToSixSquares(); // Move up to 6 squares in one-player game
    }

    /*private void RollAgain()
    {
        // Logic to roll again
        Debug.Log("Rolling again for two-player game.");
        if (playerMovement != null)
        {
            playerMovement.allowDiceRolling = true; // Enable dice rolling
        }
        else
        {
            Debug.LogWarning("PlayerMovement script is not assigned!");
        }
    }*/

    private void MoveUpToSixSquares()
    {
        if (playerMovement != null)
        {
            playerMovement.diceRoll = Random.Range(1, maxSquaresToMove + 1); // Roll a dice between 1 and 6
            playerMovement.canMove = true; // Allow the player to move
            Debug.Log("Player can move " + playerMovement.diceRoll + " squares using the scroll.");
        }
        else
        {
            Debug.LogWarning("PlayerMovement script is not assigned!");
        }
    }




    
    /*
    You find a magical scroll of teleportation. If you are playing a two-player game, you get to roll
    again. If you are playing a one player game, you get to move any number of squares you choose
    up to 6 squares
    */
}
