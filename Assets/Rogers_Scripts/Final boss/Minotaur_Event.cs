using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
You face the Minotaur – Get a scrap of paper and draw two columns. Put your name at the top of
one column and display the Minotaur at the top of the other column. Under your name display
your health which is 20 points. And under Minotaur display its strength which is 30. If you have
the magic axe, then subtract 5 points from the Minotaur’s health before beginning. His strength
will then be 25. To Battle the Minotaur. You get first attack; roll the dice and whatever number
comes up you subtract that from the Minotaur’s health. Next you roll for the Minotaur and
whatever number comes up you subtract that from your health. Continue taking turns until either
your health or the Minotaur’s health goes down to zero or less. If your health goes down to zero
you have lost the game. If the Minotaur’s health goes to zero you take possession of the token
called Minotaur Horns and continue with the game.
*/

public class Minotaur_Event : MonoBehaviour
{
    public bool rollEvent = false; // Flag to check if the player can roll the dice
    public GameObject GameOverPanel; // Reference to the player object

    public GameObject Minotaur; // Reference to the Minotaur object
    public AccessoryItems minotaurHorns; // Reference to the Minotaur Horns item
    public AccessoryItems magicAxe; // Reference to the Magic Axe item

    public bool hasMinotaurHorns = false; // Flag to check if the player has Minotaur Horns
    public Text eventText; // UI Text to display event messages

    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script

    public PlayerInventory playerInventory; // Reference to the player's inventory script

    public Player1HP playerHP; // Reference to the player's health script
    public MinotaurHP minotaurHP; // Reference to the Minotaur's health script
    // Start is called before the first frame update
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
                RollForFightMinotaur(); // Call the method to handle the Minotaur fight logic
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerMovement.allowDiceRolling = false; // Disable dice rolling while in the Minotaur event area
            if (!rollEvent) // If the player is not already rolling
            {
                eventText.text = "You have encountered a Minotaur! Press 'Space' to roll for fight."; // Display event message
                rollEvent = true; // Set the flag to indicate the player can roll

                AccessoryItems magicAxe = playerInventory.GetItemByType(AccessoryItems.specialItem.MagicAxe);
                if (magicAxe != null)
                {
                    playerInventory.EquipItem(magicAxe);
                }
            }
        }
    }

    void RollForFightMinotaur()
    {
        int playerRoll = Random.Range(1, 7); // Simulate a dice roll for the player
        int minotaurRoll = Random.Range(1, 7); // Simulate a dice roll for the Minotaur
        // Update Minotaur's health based on player's roll

        minotaurHP.currentHealth -= playerRoll; // Subtract player's roll from Minotaur's health
        playerHP.TakeDamage(minotaurRoll); // Player takes damage equal to Minotaur's roll
        minotaurHP.TakeDamage(playerRoll); // Minotaur takes damage equal to player's roll

        // Update UI texts or any other necessary components here
        eventText.text = $"You rolled {playerRoll}. Minotaur rolled {minotaurRoll}.\n" +
                         $"Minotaur Health: {minotaurHP.currentHealth}\n" +
                         $"Your Health: {playerHP.currentHealth}";

        // Check for win/loss conditions
        if (playerHP.currentHealth <= 0)
        {
            eventText.text += "\nYou have lost the game!"; // Display loss message
            rollEvent = false; // Reset the rolling event
            playerMovement.allowDiceRolling = false; // Re-enable dice rolling
            GameOverPanel.SetActive(true); // Optionally deactivate the player
        }
        else if (minotaurHP.currentHealth <= 0)
        {
            eventText.text += "\nYou defeated the Minotaur! You gain Minotaur Horns."; // Display win message
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling
            rollEvent = false; // Reset the rolling event
            Minotaur.SetActive(false); // Optionally deactivate the Minotaur
            GiveMinotaurHorns(); // Call method to give player Minotaur Horns
            // Add logic to give player Minotaur Horns item here
        }
    }

    public void GiveMinotaurHorns()
    {
        if (!hasMinotaurHorns)
        {
            playerInventory.AddItem(minotaurHorns); // Add Minotaur Horns to player's inventory
            hasMinotaurHorns = true; // Set flag to indicate player has the item
            eventText.text = "You have obtained the Minotaur Horns!"; // Display message
        }
    }
}
