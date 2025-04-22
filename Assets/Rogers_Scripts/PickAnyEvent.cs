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

    private void Update()
    {
        if(pickAnyActive) //0 - Bracelet, 1- Brush, 2- Diary, 3- Locket, 4- Mirror, 5- Pouch, 6- Ring, 7- Magic Axe
        {
            if(Input.GetKeyDown(KeyCode.Keypad0) && !items[0].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[0]);
                items[0].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) && !items[1].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[1]);
                items[1].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) && !items[2].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[2]);
                items[2].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) && !items[3].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[3]);
                items[3].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) && !items[4].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[4]);
                items[4].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) &&  !items[5].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[5]);
                items[5].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && !items[6].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[6]);
                items[6].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) && !items[7].isTaken)
            {
                pickAnyActive = false;
                inventory.AddItem(items[7]);
                items[7].isTaken = true;
                eventText.text = "";
                playerMovement.enabled = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !inventory.ZombiePoison)
        {
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0 && !EventStarted && !CheckIfAllTaken())
            {
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
