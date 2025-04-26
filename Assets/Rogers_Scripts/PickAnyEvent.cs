using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickAnyEvent : MonoBehaviour
{
    public PlayerInventory inventory;
    P_OneWayTileMovement playerMovement;
    public Text eventText;
    public AccessoryItems[] items;
    bool pickAnyActive = false;
    bool EventStarted = false;
    
    public AudioClip PickUpSound; // Sound to play when picking up an item

    private void Update()
    {

        if (pickAnyActive) // 0 - Bracelet, 1 - Brush, 2 - Diary, 3 - Locket, 4 - Mirror, 5 - Pouch, 6 - Ring, 7 - Magic Axe
        {
            for (int i = 0; i < items.Length; i++)
            {
                // Check both Keypad and Alpha keys for compatibility
                if ((Input.GetKeyDown(KeyCode.Keypad0 + i) || Input.GetKeyDown(KeyCode.Alpha0 + i)) && !items[i].isTaken)
                {
                    pickAnyActive = false;
                    inventory.AddItem(items[i]);
                    items[i].isTaken = true;
                    eventText.text = "";
                    playerMovement.enabled = true;
                    break;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !inventory.ZombiePoison)
        {
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0 && !EventStarted && !CheckIfAllTaken())
            {
                if (PickUpSound != null) // Play sound only if the player is not already rolling
                {
                    AudioSource.PlayClipAtPoint(PickUpSound, transform.position); // Play the item pickup sound
                }
                EventStarted = true;
                playerMovement = collision.GetComponent<P_OneWayTileMovement>();

                playerMovement.enabled = false;
                pickAnyEvent();
            }


        }
    }
    bool CheckIfAllTaken()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].isTaken)
            {
                return false;
            }
        }
        eventText.text = "All Items are taken!";
        return true;
    }
    void pickAnyEvent()
    {
        eventText.text = "Pick Any Item to keep:";
        for(int i = 0; i < items.Length; i++)
        {
            if (!items[i].isTaken)
            {
                eventText.text += "\n Press " + i + " for " + items[i].name;
            }
        }
        pickAnyActive = true;
    }
}
