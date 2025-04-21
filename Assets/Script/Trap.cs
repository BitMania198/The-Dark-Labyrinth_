using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
  // Duration to disable movement or ignore collisions
    public float disableDuration = 2f;

    // This method is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collide)
    {
        // Check if the object entering the trap is the player
        if (collide.CompareTag("Player"))
        {
            // Get the Player_Reset component from the player
            Player_Reset playerReset = collide.GetComponent<Player_Reset>();
            if (playerReset != null)
            {
                // Call the ResetToStart method to reset the player's position
                playerReset.ResetToStart();
            }
            else
            {
                Debug.LogWarning("Player_Reset component not found on the player!");
            }

            // Optionally, disable the trap temporarily to prevent re-triggering
            StartCoroutine(DisableTrapTemporarily());
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
