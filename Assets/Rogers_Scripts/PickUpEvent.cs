using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpEvent : MonoBehaviour
{
    PlayerInventory inventory;
    public AccessoryItems accessoryItem;
    bool ChoiceEvent = false;
    bool rollEvent = false;
    public Transform startPos;

    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script
    Transform playerPos;

    public AudioClip PickUpSound; // Sound to play when picking up the item
    public AudioClip readingSound; // Sound to play when reading the diary
    public AudioClip RefuseSound; // Sound to play when refusing to read the diary
    public AudioClip CursedSound; // Sound to play when starting the background music
    public Text eventText; // UI Text to display event messages
    // Start is called before the first frame update
    void Start()
    {
        accessoryItem.isTaken = false;
        inventory = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChoiceEvent)//player choses if they want to read with the y or n key, if y roll
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (readingSound != null) // Play reading sound if available
                {
                    AudioSource.PlayClipAtPoint(readingSound, transform.position);
                }
                print("Press 'Space' to roll dice.");
                eventText.text = "Press 'Space' to roll dice.";
                ChoiceEvent = false;
                rollEvent = true;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (RefuseSound != null) // Play refusal sound if available
                {
                    AudioSource.PlayClipAtPoint(RefuseSound, transform.position);
                }
                playerMovement.allowDiceRolling = true; // Re-enable dice rolling
                print("You decide to respect the princess' privacy.");
                eventText.text = "You decide to respect the princess' privacy.";
                inventory.AddItem(accessoryItem);
                ChoiceEvent = false;
            }
        }
        if (rollEvent)//player rolls with space bar for event
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rollEvent = false;
                RollForItem();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !inventory.ZombiePoison)
        {
            playerPos = collision.transform;
            CollectItem();
            /*
            if (!collision.GetComponent<Player_Movement_TileSize>().canMove) //If player lands on space and cant move call function
            {
                
            }
            */
        }
    }
    void CollectItem() //takes item if it isnt taken unless its diary, where you have the option read
    {
        if (!accessoryItem.isTaken)
        {
            accessoryItem.isTaken = true;
            if (accessoryItem.ItemType == AccessoryItems.specialItem.Diary)
            {
                print("You have found the princess' diary! Read it?\nY/N");
                eventText.text = "You have found the princess' diary! Read it?\nY/N"; // Display message to read the diary
                playerMovement.allowDiceRolling = false; // Disable dice rolling while making a choice
                ChoiceEvent = true;
            }
            else
            {
                if (PickUpSound != null) // Play pickup sound if available
                {
                    AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
                }
                print("You have found the " + accessoryItem.name + "!");
                eventText.text = "You have found the " + accessoryItem.name + "!"; // Display message for finding the item
                inventory.AddItem(accessoryItem);
                playerMovement.allowDiceRolling = true; // Re-enable dice rolling after collecting the item
            }
        }
    }

    void RollForItem()
    {
        int diceRoll = Random.Range(1, 6);
        if (diceRoll == 1 || diceRoll == 2)
        {
            if (PickUpSound != null) // Play cursed sound if available
            {
                AudioSource.PlayClipAtPoint(CursedSound, transform.position);
            }
            print("You find nothing interesting, but you still bring her diary");
            eventText.text = "You find nothing interesting, but you still bring her diary"; // Display message for finding nothing interesting
            inventory.AddItem(accessoryItem);
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after rolling the dice
        }
        else if (diceRoll == 3 || diceRoll == 4)
        {
            if (PickUpSound != null) // Play cursed sound if available
            {
                AudioSource.PlayClipAtPoint(CursedSound, transform.position);
            }
            print("You find a horrifing secret which you wish you never knew.\n You drop the diary and head to the start");
            eventText.text = "You find a horrifying secret which you wish you never knew.\nYou drop the diary and head to the start"; // Display message for finding a horrifying secret
                                                                                                                                      //insert sending to start code here (needs to be before accesoryItem code
            P_OneWayTileMovement playerMovement = playerPos.GetComponent<P_OneWayTileMovement>();
            playerMovement.transform.position = startPos.position; // Move player to start position
            playerMovement.playerPos = startPos.position; // Update player's position in the movement script
            accessoryItem.isTaken = false;
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after rolling the dice
        }
        else
        {
            if (CursedSound != null) // Play cursed sound if available
            {
                AudioSource.PlayClipAtPoint(CursedSound, transform.position);
            }
            print("You find the location of one of her most prized possetions!");
            eventText.text = "You find the location of one of her most prized possessions!"; // Display message for finding a prized possession
            inventory.AddItem(accessoryItem);
            playerMovement.allowDiceRolling = true; // Re-enable dice rolling after rolling the dice

            for (int i = 0; i < accessoryItem.AllItems.Length; i++)
            {
                if (!accessoryItem.AllItems[i].isTaken && accessoryItem.AllItems[i].ItemType != AccessoryItems.specialItem.MinotaurHorns)
                {
                    inventory.AddItem(accessoryItem.AllItems[i]);
                    return;
                }
            }

        }
    }

    void HandleLostItem()
    {
        if (inventory.HasItems())
        {
            AccessoryItems lostItem = inventory.RemoveRandomItem();
            if (lostItem != null)
            {
                eventText.text = $"You lost your {lostItem.ItemName}!";
            }
            else
            {
                eventText.text = "You lost an item, but no items were found in your inventory.";
            }
        }
        else
        {
            eventText.text = "You have no items to lose.";
        }
    }
}
