using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    List<AccessoryItems> possesions = new List<AccessoryItems>();
    [HideInInspector]
    public bool ZombiePoison = false;

    public Image[] inven_sprites;
    Turnbased turn;
    [HideInInspector]
    public int playerNum;

    private void Start()
    {
        for (int i = 0; i < inven_sprites.Length; i++)
        {
            inven_sprites[i].enabled = false;
        }
        turn = GameObject.FindGameObjectWithTag("TurnBased").GetComponent<Turnbased>();
        if (turn != null && turn.p1_Inventory == null)
        {
            turn.p1_Inventory = this;
            playerNum = 1;
        }
        else if(turn != null)
        {
            turn.p2_Inventory = this;
            playerNum = 2;
            turn.TwoPlayers = true;
        }
    }
    public void AddItem(AccessoryItems addedItem)
    {
        possesions.Add(addedItem);
        updateUI();
    }
    void updateUI()
    {
        for (int i = 0; i < possesions.Count; i++)
        {
            inven_sprites[i].enabled = true;
            inven_sprites[i].sprite = possesions[i].ItemSprite;
        }
    }

    public bool HasItems()
    {
        return possesions.Count > 0;
    }

    public AccessoryItems RemoveRandomItem()
    {
        if (possesions.Count > 0)
        {
            int randomIndex = Random.Range(0, possesions.Count);
            AccessoryItems removedItem = possesions[randomIndex];
            possesions.RemoveAt(randomIndex);
            updateUI();
            return removedItem; // Return the removed item
        }
        else
        {
            Debug.LogWarning("No items to remove from inventory.");
            return null; // Return null if no items are available
        }
    }

    public AccessoryItems GetItemByType(AccessoryItems.specialItem itemType)
    {
        foreach (AccessoryItems item in possesions)
        {
            if (item.ItemType == itemType)
            {
                return item; // Return the first item that matches the type
            }
        }
        return null; // Return null if no matching item is found
    }

    public int GetPossessionCount()
    {
        return possesions.Count; // Return the number of items in the inventory
    }

    public void EquipItem(AccessoryItems item)
    {
        if (item == null)
        {
            Debug.LogWarning("No item to equip.");
            return;
        }

        switch (item.ItemType)
        {
            case AccessoryItems.specialItem.MagicAxe:
                TriggerMagicAxeEvent();
                break;

            case AccessoryItems.specialItem.MinotaurHorns:
                Debug.Log("Minotaur Horns equipped. No special event triggered.");
                break;

            case AccessoryItems.specialItem.Diary:
                Debug.Log("Diary equipped. Trigger diary-specific event if needed.");
                break;

            default:
                Debug.Log("Equipped a regular item.");
                break;
        }
    }

    private void TriggerMagicAxeEvent()
    {
        Debug.Log("Magic Axe equipped! You feel a surge of power.");
        // Example: Boost player's attack power or trigger a visual effect
        MinotaurHP minotaurHP = FindObjectOfType<MinotaurHP>();
        if (minotaurHP != null)
        {
            minotaurHP.maxHealth -= 5; // Reduce Minotaur's health by 5 points
            minotaurHP.currentHealth -= 5; // Subtract 5 from Minotaur's health if player has Magic Axe
        }
        else
        {
            Debug.LogWarning("MinotaurHP component not found in the scene.");
        }

        // Optionally, trigger a UI message or animation
        // Example: Show a message on the screen
        GameObject uiMessage = GameObject.Find("UIMessage");
        if (uiMessage != null)
        {
            Text messageText = uiMessage.GetComponent<Text>();
            if (messageText != null)
            {
                messageText.text = "You equipped the Magic Axe! Your attack power has increased.";
            }
        }
    }
}
