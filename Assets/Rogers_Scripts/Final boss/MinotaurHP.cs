using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinotaurHP : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider
    public int maxHealth = 30; // Max health of the Minotaur
    public int currentHealth; // Current health of the Minotaur

    public Text statusText; // UI Text to display status messages


    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        UpdateHealthUI(); // Update the UI at the start
    }

    // Method to apply damage to the Minotaur
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract damage from current health
        if (currentHealth < 0) currentHealth = 0; // Ensure health doesn't go below zero
        UpdateHealthUI(); // Update the UI after taking damage

        if (currentHealth <= 0)
        {
            statusText.text = "You have defeated the Minotaur!"; // Display victory message
        }
    }

    public void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth; // Update the health slider
    }
}
