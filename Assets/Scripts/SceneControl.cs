using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public GameObject gameOverScreen;

    // Button for start game (start menu -> game screen)
    public void startGame()
    {
        SceneManager.LoadScene("RunGameScene");
    }

    // Logic for when game is over
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    // Button for playing again (end menu -> game screen)
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Button for return to main menu (end menu -> start menu)
    public void toMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
