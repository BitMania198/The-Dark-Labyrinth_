using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spider_Event : MonoBehaviour
{
    bool rollEvent = false;
    public GameObject Player; // Reference to the player object

    private AccessoryItems accessoryItem; // Reference to the accessory item
    public PlayerInventory inventory; // Reference to the player's inventory

    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script

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
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = collision.gameObject; // Get the player object
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
        }
    }

    void RollForfightSpider()
    {
        
        int diceRoll = Random.Range(1, 7); // Simulate a dice roll between 1 and 5
        PlayerInventory playerInventory = Player.GetComponent<PlayerInventory>();


        if (diceRoll == 1 || diceRoll == 2 || diceRoll == 3 || diceRoll == 4)
        {
            eventText.text = "You win the fight against the spider";
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
        }
        else if (diceRoll == 5)
        {
        if (playerInventory != null && playerInventory.HasItems())
        {
            AccessoryItems lostItem = playerInventory.RemoveRandomItem(); // Remove a random item
            if (lostItem != null)
            {
                eventText.text = $"You got bitten by the spider and lost your {lostItem.ItemName}!";
            }
            else
            {
                eventText.text = "You got bitten by the spider, but no items were lost.";
            }
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
        }
        else
        {
            eventText.text = "You got bitten by the spider! But you have no items to lose.";
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
            MovePlayerToStart(); // Move the player to the start position
        }
        }
        else if (diceRoll == 6)
        {
            Debug.Log("The Spider has killed you! You have lost the game");
            eventText.text = "The Spider has killed you! You have lost the game.";
            Player.SetActive(false); // Deactivate the player object to signify game over
            GameOver(); // Call the GameOver method to handle game over logic

        }
        rollEvent = false; // Reset the flag after the fight
    }

    void MovePlayerToStart()
    {
        Player_Reset playerReset = Player.GetComponent<Player_Reset>();
        if (playerReset != null)
        {
            // Call the ResetToStart method to reset the player's position
            playerReset.ResetToStart();
        }
        else
        {
            Debug.LogWarning("Player_Reset component not found on the player!");
        }
    }

    void GameOver()
    {
        // Example logic for game over
        Debug.Log("Game Over! Restart the game or quit.");
        eventText.text = "Game Over! Restart the game or quit."; // Display game over message
        // Add additional game-over handling logic here
    }
}
