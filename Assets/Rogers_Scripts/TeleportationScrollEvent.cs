using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportationScrollEvent : MonoBehaviour
{
    public Text eventText;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0)
            {
                eventText.text = "You found the scroll of teleportation! You can move 6 extra spaces!";
                P_OneWayTileMovement movement = collision.GetComponent<P_OneWayTileMovement>();
                movement.DiceRoll = 6;
                movement.canMove = true;
                
            }
        }
    }
}
