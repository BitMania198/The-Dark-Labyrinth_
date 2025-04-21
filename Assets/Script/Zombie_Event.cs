using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_Event : MonoBehaviour
{
    bool rollEvent = false;
    public GameObject Player;

    public Text eventText; // UI Text to display event messages

    public Player_Movement_TileSize playerMovement;

    public GameObject Camera;

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
                RollForfightZomble(); // Call the method to handle the zombie fight logic
                rollEvent = false; // Reset the flag after rolling
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = collision.gameObject; // Get the player object
            playerMovement.allowDiceRolling = false; // Disable dice rolling while in the zombie event area
            if (!rollEvent) // If the player is not already rolling
            {
                Debug.Log("You have encountered a zombie! Press 'Space' to roll for fight.");
                eventText.text = "You have encountered a zombie! Press 'Space' to roll for fight."; // Display event message
                rollEvent = true; // Set the flag to indicate the player can roll
            }
        }
        else if (collision.tag != "Player" && rollEvent) // If the player leaves the trigger area
        {
            rollEvent = false; // Reset the flag if the player is not in the trigger area
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling
            eventText.text = ""; // Clear the event text

        }
    }

    void RollForfightZomble()
    {
        int diceRoll = Random.Range(1, 6); // Simulate a dice roll between 1 and 5
        if (diceRoll == 1)
        {
            // Player loses the fight
            Debug.Log("You lost the fight against the zombie!");
            eventText.text = "You lost the fight against the zombie!"; // Display loss message
            Player.SetActive(false); // Example action: deactivate player
            rollEvent = false; // Reset the roll event flag
            // Handle losing logic here, e.g., reduce health, respawn, etc.
        }
        else if (diceRoll == 2)
        {
            // Player wins the fight
            Debug.Log("You won the fight against the zombie! But you are injured. you need to go to the number 14 space to heal.");
            eventText.text = "You won the fight against the zombie! But you are injured. You need to go to the number 14 space to heal."; // Display win message
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after the fight
            rollEvent = false; // Reset the roll event flag
            // Handle winning logic here, e.g., gain items, experience, etc.
        }
        else if (diceRoll == 3 || diceRoll == 4 || diceRoll == 5)
        {
            // Player escapes
            Debug.Log("You managed to defeat the zombie!");
            eventText.text = "You managed to defeat the zombie! You can now escape."; // Display escape message
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after escaping
            rollEvent = false; // Reset the roll event flag
            // Handle escaping logic here, e.g., return to start position, etc.
        }
        else if (diceRoll == 6)
        {
            // Player rolls a 6, which is a special case
            Debug.Log("You rolled a 6! You can choose to fight or escape.");
            eventText.text = "You rolled a 6! You can choose to fight or escape."; // Display special case message
            playerMovement.allowDiceRolling = true; // Disable dice rolling while making a choice
            rollEvent = false; // Reset the roll event flag
            // Implement logic for player choice here, e.g., show UI options
            // For example, you could set a flag to indicate the player can choose
        }
    }

    
}
