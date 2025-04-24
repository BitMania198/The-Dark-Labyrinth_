using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spider_Event : MonoBehaviour
{
    bool rollEvent = false;
    private AccessoryItems accessoryItem; // Reference to the accessory item
    public PlayerInventory inventory; // Reference to the player's inventory

    public Transform startPos; // Reference to the starting position for the player
    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script

    public GameObject GameOverPanel; // Reference to the game over panel

    Transform playerPos;

    public Text eventText; // UI Text to display event messages

    void Start()
    {
        // Initialize any necessary variables or states here
        rollEvent = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (rollEvent) // Check if the player can roll the dice or make a choice
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Use space key to roll the dice
            {
                RollForfightSpider(); // Call the method to handle the zombie fight logic
                rollEvent = false; // Reset the flag after rolling
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMovement.allowDiceRolling = false; // Disable dice rolling while in the spider event area
            if (!rollEvent) // If the player is not already rolling
            {
                eventText.text = "You have encountered a spider! Press 'Space' to roll for fight."; // Display event message
                rollEvent = true; // Set the flag to indicate the player can roll
            }
        }
        else if (collision.tag != "Player" && rollEvent) // If the player leaves the trigger area
        {
            rollEvent = false; // Reset the flag if the player is not in the trigger area
            Debug.Log("You left the Spider space. You can no longer roll for fight.");
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling when leaving the spider area
            eventText.text = ""; // Clear the event text when leaving the spider area
        }
    }

    void RollForfightSpider()
    {
        
        int diceRoll = Random.Range(1, 7); // Simulate a dice roll between 1 and 6

        PlayerInventory playerInventory = GameObject.FindObjectOfType<PlayerInventory>(); // Find the PlayerInventory in the scene


        if (diceRoll == 1 || diceRoll == 2 || diceRoll == 3 || diceRoll == 4)
        {
            eventText.text = "You win the fight against the spider";
            rollEvent = false; // Reset the flag after the fight
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
        }
        else if (diceRoll == 5)
        {
            if (playerInventory != null && playerInventory.HasItems())
            {
                eventText.text = "You got bitten by the spider! You lose an item.";
                // Remove a random item from the player's inventory
                AccessoryItems lostItem = playerInventory.RemoveRandomItem();
                if (lostItem != null)
                {
                    eventText.text += $"\nYou lost: {lostItem.ItemName}"; // Display the lost item name
                }
                else
                {
                    eventText.text += "\nYou have no items to lose."; // If no items to lose
                }
                playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
            }
            else
            {
                eventText.text = "You got bitten by the spider! But you have no items to lose.";
                rollEvent = false; // Reset the flag after the fight
                playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
                MovePlayerToStart(); // Move the player to the start position
            }
        }
        else if (diceRoll == 6)
        {
            Debug.Log("The Spider has killed you! You have lost the game");
            eventText.text = "The Spider has killed you! You have lost the game.";
            GameOverPanel.SetActive(true); // Activate the game over panel
            rollEvent = false; // Reset the flag after the fight
            GameOver(); // Call the GameOver method to handle game over logic

        }
        rollEvent = false; // Reset the flag after the fight
    }

    void MovePlayerToStart()
    {
        if (playerMovement != null && startPos != null)
        {
            playerMovement.transform.position = startPos.position; // Move player to start position
            playerMovement.playerPos = startPos.position; // Update player's position in the movement script
            eventText.text += "\nYou have been moved back to the start position."; // Display message for moving back to start
        }
        else
        {
            Debug.LogWarning("Player movement or start position is not set correctly.");
        }
    }

    void GameOver()
    {
        // Example logic for game over
        Debug.Log("Game Over! Restart the game or quit.");
        playerMovement.allowDiceRolling = false; // Disable dice rolling on game over
        // Add additional game-over handling logic here
    }
}
