using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonCuringEvent : MonoBehaviour
{
    public PlayerInventory inventory;
    public Text eventText;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && inventory.ZombiePoison)
        {
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0)
            {
                eventText.text = "There is a small hole in the wall that gives off a gas. It has cured your poison!";
                inventory.ZombiePoison = false;
            }


        }
    }
}
