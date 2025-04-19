using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement_TileSize : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    private int diceRoll = 0;
    public bool canMove;

    public int maxDice = 6; // Maximum value of the dice
    public int minDice = 1; // Minimum value of the dice

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; // Detach movePoint from the player
        canMove = false; // Initially, the player cannot move   
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            // If the player cannot move, allow them to roll the dice by pressing space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RollDice();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        diceRoll--;
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        diceRoll--;
                    }
                }
            }

            if (diceRoll <= 0)
            {
                canMove = false; // Disable movement when the dice roll reaches zero
                Debug.Log("Movement disabled, dice roll exhausted."); // Log when movement is disabled
            }
        }
    }

    private void RollDice()
    {
        diceRoll = Random.Range(minDice, maxDice + 1); // Generate a random number between minDice and maxDice
        canMove = true; // Allow movement
        Debug.Log("Dice rolled: " + diceRoll); // Log the dice roll value
    }
}
