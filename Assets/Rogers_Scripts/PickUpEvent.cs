using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEvent : MonoBehaviour
{
    PlayerInventory inventory;
    public AccessoryItems accessoryItem;
    bool ChoiceEvent = false;
    bool rollEvent = false;
    public Transform startPos;
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        accessoryItem.isTaken = false;
        inventory = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ChoiceEvent)//player choses if they want to read with the y or n key, if y roll
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                print("Press 'Space' to roll dice.");
                ChoiceEvent = false;
                rollEvent = true;
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                print("You decide to respect the princess' privacy.");
                inventory.AddItem(accessoryItem);
                ChoiceEvent = false;
            }
        }
        if(rollEvent)//player rolls with space bar for event
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rollEvent = false;
                RollForItem();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
        if(!accessoryItem.isTaken)
        {
            accessoryItem.isTaken = true;
            if(accessoryItem.ItemType == AccessoryItems.specialItem.Diary)
            {
                print("You have found the princess' diary! Read it?\nY/N");
                ChoiceEvent = true;
            }
            else
            {
                print("You have found the " +  accessoryItem.name + "!");
                inventory.AddItem(accessoryItem);
            }
        }
    }

    void RollForItem()
    {
        int diceRoll = Random.Range(1 , 6);
        if(diceRoll  == 1 || diceRoll == 2)
        {
            print("You find nothing interesting, but you still bring her diary");
            inventory.AddItem(accessoryItem);
        }
        else if (diceRoll == 3 || diceRoll == 4)
        {
            print("You find a horrifing secret which you wish you never knew.\n You drop the diary and head to the start");
            //insert sending to start code here (needs to be before accesoryItem code
            playerPos.position = startPos.position;
            
            accessoryItem.isTaken = false;
        }
        else
        {
            print("You find the location of one of her most prized possetions!");
            inventory.AddItem(accessoryItem);
            
            for(int i = 0; i < accessoryItem.AllItems.Length; i++)
            {
                if (!accessoryItem.AllItems[i].isTaken && accessoryItem.AllItems[i].ItemType != AccessoryItems.specialItem.MinotaurHorns)
                {
                    inventory.AddItem(accessoryItem.AllItems[i]);
                    return;
                }
            }

        }
    }
}
