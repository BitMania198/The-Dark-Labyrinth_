using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public int maxdice = 6; // Maximum value of the dice
    public int mindice = 1; // Minimum value of the dice
    public Player_Movement_TileSize playerMovement; // Reference to the Player_Movement_TileSize script
    public int RollDices()
    {
        int diceroll = Random.Range(mindice, maxdice + 1); // Generate a random number between mindice and maxdice
        Debug.Log("Dice rolled: " + diceroll); // Log the dice roll value
        playerMovement.moveSpeed = diceroll; // Set the player's movement speed to the rolled value
        return diceroll; // Return the rolled value
    }
}
