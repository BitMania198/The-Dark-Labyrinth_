using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_OneWayTileMovement : MonoBehaviour
{
    public IsMoveable[] isMoveables; // Up = 0 Right = 1 Down = 2 Left = 3
    public MovementTile[] movemementTile;
    public Sprite[] movementTileSprites;

    public Collider2D playerCollider; // Reference to the player's collider

    public bool allowDiceRolling = true; // Flag to allow or disallow dice rolling

    public Text diceText; // Reference to the UI Text to display dice roll

    public Transform startTransform; // Starting position of the player

    public Vector3 playerPos;
    Animator animator;

    [HideInInspector]
    public int DiceRoll;
    bool canMove;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        isMoving = false;
        canMove = false;
        playerPos = transform.position;
        DisableMovementTiles();
        playerCollider = GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            playerCollider.enabled = true; // Ensure the player's collider is enabled
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Dice Roll: " + DiceRoll + "\nCan Move: " + canMove);
        if (!canMove)
        {
            if (allowDiceRolling)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    canMove = true;
                    DiceRoll = Random.Range(1, 6);
                    if (diceText != null)
                    {
                        diceText.text = "Dice Roll: " + DiceRoll; // Display the dice roll
                    }
                }
            }
        }
        else
        {
            if (playerCollider != null)
            {
                playerCollider.enabled = false; // Ensure the player's collider is enabled
            }

            if (canMove && !isMoving && isntStuck()) // Up = 0 Right = 1 Down = 2 Left = 3
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isMoving = true;
                    StartCoroutine(MoveToTile(0));
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isMoving = true;
                    StartCoroutine(MoveToTile(3));
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isMoving = true;
                    StartCoroutine(MoveToTile(2));
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isMoving = true;
                    StartCoroutine(MoveToTile(1));
                }
            }
        }
    }
        public void ResetToStart()
    {
        if (startTransform != null)
        {
            // Reset position
            transform.position = startTransform.position;

            // Stop movement
            Rigidbody2D playerRigidbody = GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.angularVelocity = 0f;
            }

            // Reset movement flags
            canMove = false;
            isMoving = false;
            DiceRoll = 0;

            // Disable movement tiles
            DisableMovementTiles();

            // Re-enable collider
            if (playerCollider != null)
            {
                playerCollider.enabled = true;
            }

            Debug.Log("Player reset to start position: " + startTransform.position);
        }
        else
        {
            Debug.LogWarning("Start Transform is not assigned!");
        }
    }

    IEnumerator MoveToTile(int moveID)
    {
        if (isMoveables[moveID].isMoveable)
        {
            DiceRoll--;
            int tileNum = GetUnusedTile();
            ToggleMovementAnimation(moveID, true);
            movemementTile[tileNum].sprite.sprite = movementTileSprites[moveID];
            yield return new WaitForSeconds(1f);
            Vector3 temp = transform.position;
            ToggleMovementAnimation(moveID, false);
            UpdatePos(moveID, tileNum);
            movemementTile[tileNum].isActive = true;
        }
        if (DiceRoll <= 0)
        {
            canMove = false;
            DisableMovementTiles();
        }
        isMoving = false;

    }
    void DisableMovementTiles()
    {
        for (int i = 0; i < movemementTile.Length; i++)
        {
            movemementTile[i].isActive = false;
            if (playerCollider != null)
            {
                playerCollider.enabled = true; // Re-enable the player's collider
            }
        }
    }
    int GetUnusedTile()
    {
        for (int i = 0; i < movemementTile.Length; i++)
        {
            if (!movemementTile[i].isActive)
            {
                return i;

            }
        }
        return 6;
    }
    void ToggleMovementAnimation(int animID, bool toggleVal)
    {
        switch (animID)
        {
            case 0:
                animator.SetBool("MoveUp", toggleVal);
                break;
            case 1:
                animator.SetBool("MoveRight", toggleVal);
                break;
            case 2:
                animator.SetBool("MoveDown", toggleVal);
                break;
            case 3:
                animator.SetBool("MoveLeft", toggleVal);
                break;
        }
    }
    void UpdatePos(int directionNum, int tileNum)
    {
        Vector2 pos;
        switch (directionNum)
        {
            case 0:
                pos = new Vector2(transform.position.x, transform.position.y + 1);
                transform.position = pos;
                movemementTile[tileNum].transform.position = isMoveables[2].transform.position;
                break;
            case 1:
                pos = new Vector2(transform.position.x + 1, transform.position.y);
                transform.position = pos;
                movemementTile[tileNum].transform.position = isMoveables[3].transform.position;
                break;
            case 2:
                pos = new Vector2(transform.position.x, transform.position.y - 1);
                transform.position = pos;
                movemementTile[tileNum].transform.position = isMoveables[0].transform.position;
                break;
            case 3:
                pos = new Vector2(transform.position.x - 1, transform.position.y);
                transform.position = pos;
                movemementTile[tileNum].transform.position = isMoveables[1].transform.position;
                break;
        }
    }
    public bool isntStuck()
    {
        for (int i = 0; i < isMoveables.Length; i++)
        {
            if (isMoveables[i].isMoveable) return true;
        }
        DisableMovementTiles();
        DiceRoll = 0;
        canMove = false;
        return false;
    }
}
