using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Screen : MonoBehaviour
{
    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene("PickuUpTest");
    }

    public void QuitGame()
    {
        // Quit the application
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene instead of quitting
        Debug.Log("Game has been quit."); // Log message for debugging purposes
    }
}
