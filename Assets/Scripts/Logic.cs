using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    // Button for start game (start menu -> game screen)
    public void startGame()
    {
        SceneManager.LoadScene("RunGameScene");
    }

    // Button for playing again (end menu -> game screen)
    public void playAgain()
    {

    }

    // Button for return to main menu (end menu -> start menu)
    public void toMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
