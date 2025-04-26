using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnbased : MonoBehaviour
{
    [HideInInspector]
    public P_OneWayTileMovement p1_Movement;
    [HideInInspector]
    public P_OneWayTileMovement p2_Movement;
    [HideInInspector]
    public PlayerInventory p1_Inventory;
    [HideInInspector]
    public PlayerInventory p2_Inventory;
    [HideInInspector]
    public bool TwoPlayers = false;

    private void Start()
    {
        TwoPlayers = false;
    }
    private void Update()
    {
        if (TwoPlayers) print("There are 2 players");
    }
    public void EndTurn(int pNum)
    {
        if (pNum == 1)
        {
            p1_Movement.enabled = false;
            p2_Movement.enabled = true;
            p1_Inventory.gameObject.tag = "PlayerOne";
            p2_Inventory.gameObject.tag = "Untagged";
        }
        else
        {
            p1_Movement.enabled = true;
            p2_Movement.enabled = false;
            p1_Inventory.gameObject.tag = "Untagged";
            p2_Inventory.gameObject.tag = "PlayerOne";
        }
    }
}
