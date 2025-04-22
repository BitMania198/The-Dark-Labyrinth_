using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Princness_Winner : MonoBehaviour
{
    public PlayerInventory playerInventory; // Reference to the player's inventory
    public Transform playerStartPos; // Reference to the player's start position
    public Text eventText; // UI Text to display messages
    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script

    public GameObject GameWinPanel; // Reference to the Game Win panel

    void Start()
    {
        // Initialize any necessary variables or states here
        GameWinPanel.SetActive(false); // Ensure the GameWinPanel is inactive at the start
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckWinCondition();
            playerMovement.allowDiceRolling = false; // Disable dice rolling while in the win event area
        }
    }

    void CheckWinCondition()
    {
        // Check if the player has the Minotaur's Horns
        AccessoryItems minotaurHorns = playerInventory.GetItemByType(AccessoryItems.specialItem.MinotaurHorns);
        if (minotaurHorns != null)
        {
            eventText.text = "You have the Minotaur's Horns! You have won the heart of the princess and the game!";
            GameWinPanel.SetActive(true); // Show the Game Win panel
            Debug.Log("Player wins the game with the Minotaur's Horns!");
            return;
        }

        // Check the number of possessions
        int possessionCount = playerInventory.GetPossessionCount();
        if (possessionCount >= 6)
        {
            eventText.text = "You have six or more possessions! You have won the heart of the princess and the game!";
            GameWinPanel.SetActive(true); // Show the Game Win panel
            Debug.Log("Player wins the game with six or more possessions!");
        }
        else
        {
            // Roll the dice
            int diceRoll = Random.Range(1, 7);
            if (diceRoll > possessionCount)
            {
                eventText.text = $"You rolled a {diceRoll}, which is higher than your {possessionCount} possessions. You have not won her heart. Returning to start.";
                Debug.Log("Player did not win. Moving to start position.");
                playerMovement.transform.position = playerStartPos.position; // Move player to start position
                playerMovement.playerPos = playerStartPos.position; // Update player's position in the movement script
            }
            else
            {
                eventText.text = $"You rolled a {diceRoll}, which is not higher than your {possessionCount} possessions. but you still won her heart.";
                GameWinPanel.SetActive(true); // Show the Game Win panel
                Debug.Log("Player may continue the game.");
            }
        }
    }
}
