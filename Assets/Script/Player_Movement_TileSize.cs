using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement_TileSize : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    private int diceRoll = -1;
    public bool canMove;

    public bool allowDiceRolling = true; // Flag to allow or disallow dice rolling

    public int maxDice = 6; // Maximum value of the dice
    public int minDice = 1; // Minimum value of the dice

    public Text diceText; // Reference to the UI Text to display dice roll

    public Collider2D playerCollider; // Reference to the player's collider
    public Collider2D ItemCollider; // Reference to the item collider

    public GameObject ItemObject;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; // Detach movePoint from the player
        canMove = false; // Initially, the player cannot move
        ItemObject.SetActive(false); // Ensure the item object is inactive at the start   

        playerCollider = GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            playerCollider.enabled = true; // Ensure the player's collider is enabled
        }
        if (ItemCollider != null)
        {
            ItemCollider.enabled = true; // Disable the item collider if it exists
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            if (allowDiceRolling)
            {
                if (diceText != null)
                {
                    diceText.text = "Press Left Mouse to Roll Dice"; // Display message to roll the dice
                    if (Input.GetMouseButtonDown(0)) // Check for space key press to roll the dice
                    {
                        RollDice(); // Call the method to roll the dice
                    }
                }
                else
                {
                    Debug.LogWarning("Dice Text UI element is not assigned!"); // Log a warning if diceText is not assigned
                }
            }
        }
        else
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
            if (ItemCollider != null)
            {
                ItemCollider.enabled = false; // Disable the item collider while moving
            }

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

            if (diceRoll <= -1)
            {
                canMove = false; // Disable movement when the dice roll reaches zero
                diceText.text = "You have finished moving!"; // Display message when movement is finished
                if (playerCollider != null)
                {
                    playerCollider.enabled = true; // Re-enable the player's collider
                }
                if (ItemCollider != null)
                {
                    ItemCollider.enabled = true; // Re-enable the item collider
                }
            }
        }
    }

    private void RollDice()
    {
        diceRoll = Random.Range(minDice, maxDice + 1); // Generate a random number between minDice and maxDice
        canMove = true; // Allow movement
        if (diceText != null)
        {
            diceText.text = "Dice Roll: " + diceRoll; // Update the UI Text with the rolled value
        }
        else
        {
            Debug.LogWarning("Dice Text UI element is not assigned!"); // Log a warning if diceText is not assigned
        }
    }
}
