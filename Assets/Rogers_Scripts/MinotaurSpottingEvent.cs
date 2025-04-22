using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinotaurSpottingEvent : MonoBehaviour
{
    public AccessoryItems MagicAxe;
    public Transform MinotaurLocation;
    public Text eventText;
    bool triggeredOnce = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<P_OneWayTileMovement>().DiceRoll <= 0 && !triggeredOnce)
            {
                triggeredOnce = true;
                eventText.text = "2.\tYou have made a big mistake, and the Minotaur has spotted you! You must move your piece to one of the squares numbered 1 " +
                    "and then follow the directions for battling the Minotaur.  But along the way you have found the Magic Axe " +
                    "and can immediately take it even if someone else possesses it. ";
                GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<PlayerInventory>().AddItem(MagicAxe);
                collision.transform.position = MinotaurLocation.position;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggeredOnce = false;
    }
}
