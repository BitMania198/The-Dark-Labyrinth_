using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
  // Duration to disable movement or ignore collisions
    public float disableDuration = 2f;
    public Transform startPos;

    public P_OneWayTileMovement playerMovement; // Reference to the player's movement script
    Transform playerPos;

    // This method is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the collider belongs to the player
        {
            playerPos = collision.transform; // Assign the player's transform to playerPos
            P_OneWayTileMovement playerMovement = playerPos.GetComponent<P_OneWayTileMovement>();
        
            if (playerMovement != null) // Ensure playerMovement is not null
            {
                playerMovement.transform.position = startPos.position; // Move player to start position
                playerMovement.playerPos = startPos.position; // Update player's position in the movement script

                // Optionally, disable the trap temporarily to prevent re-triggering
                StartCoroutine(DisableTrapTemporarily());
            }
            else
        {
            Debug.LogError("P_OneWayTileMovement component not found on the player.");
        }
        }
    }

    // Coroutine to disable the trap temporarily
    private IEnumerator DisableTrapTemporarily()
    {
        Collider2D trapCollider = GetComponent<Collider2D>();
        if (trapCollider != null)
        {
            trapCollider.enabled = false; // Disable the trap
            yield return new WaitForSeconds(disableDuration);
            trapCollider.enabled = true; // Re-enable the trap
        }
    }
}
