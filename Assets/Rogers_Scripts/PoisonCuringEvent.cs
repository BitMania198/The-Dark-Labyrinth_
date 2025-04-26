using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonCuringEvent : MonoBehaviour
{
    public PlayerInventory inventory;
    public Text eventText;

    public AudioClip CureSound; // Sound to play when curing poison
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && inventory.ZombiePoison)
        {
            if (CureSound != null) // Play cure sound if available
            {
                AudioSource.PlayClipAtPoint(CureSound, transform.position);
            }
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0)
            {
                eventText.text = "There is a small hole in the wall that gives off a gas. It has cured your poison!";
                inventory.ZombiePoison = false;
            }


        }
    }
}
