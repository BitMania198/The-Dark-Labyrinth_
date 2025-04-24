using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap_endless_Corridor : MonoBehaviour
{
    public bool rollEvent = false; // Flag to indicate if the player can roll the dice or make a choice

    public Transform startPos; // Position to reset the player to

    public Transform TrapPos; // Position of the trap

    public Text eventText; // UI Text to display event messages

    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script

    bool triggeredOnce = false; // Flag to ensure the trap is triggered only once

    void Start()
    {
        rollEvent = false; // Initialize the rollEvent flag to false   
    }

    // Update is called once per frame
    void Update()
    {
        if (rollEvent) // Check if the player can roll the dice or make a choice
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Use space key to roll the dice
            {
                RollToEscape(); // Call the method to handle rolling the dice to escape the trap
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the collider belongs to the player
        {
            playerMovement.allowDiceRolling = false; // Disable dice rolling while in the trap area
            if (!triggeredOnce) // Ensure the trap is triggered only once
            {
                triggeredOnce = true; // Set the flag to true
                playerMovement = collision.GetComponent<P_OneWayTileMovement>(); // Get the player's movement script

                if (playerMovement != null) // Ensure playerMovement is not null
                {
                    playerMovement.transform.position = TrapPos.position; // Move player to start position
                    playerMovement.playerPos = TrapPos.position; // Update player's position in the movement script

                    eventText.text = "You have fallen into an endless corridor! You must roll the dice to escape."; // Display event message
                    rollEvent = true; // Set rollEvent to true to allow rolling dice or making a choice
                }
                else
                {
                    Debug.LogError("P_OneWayTileMovement component not found on the player.");
                }
            }
        }
    }

    void RollToEscape()
    {
        int diceRoll = Random.Range(1, 6);
        if (diceRoll == 1 || diceRoll == 2 || diceRoll == 3)
        {
            // if they roll 1, 2, or 3, they would be stuck in the trap
            eventText.text = "You rolled a " + diceRoll + ". You are still trapped in the endless corridor!";
            rollEvent = true; // Allow the player to roll again
        }
        else if (diceRoll == 4 || diceRoll == 5 || diceRoll == 6)
        {
            // if they roll 4 or 5, they escape the trap
            eventText.text = "You rolled a " + diceRoll + ". You have escaped the endless corridor!";
            playerMovement.transform.position = startPos.position; // Move player back to start position
            playerMovement.playerPos = startPos.position; // Update player's position in the movement script
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling
            rollEvent = false; // Reset rollEvent to false
            triggeredOnce = false; // Reset triggeredOnce to allow re-triggering the trap
        }
    }


}
