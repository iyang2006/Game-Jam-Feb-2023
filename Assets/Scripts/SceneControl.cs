using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    GameObject canvas;

    void Start()
    {
        canvas = transform.GetChild(0).gameObject;
    }

    // Button for start game (start menu -> game screen)
    public void StartGame()
    {
        SceneManager.LoadScene("RunGameScene");
    }

    // Logic for when game is over
    public void GameOver()
    {
        canvas.SetActive(true);
    }

    // Reactivate canvases/scenes
    public void GameDeactivate()
    {
        // Debug.Log("deactivated1");
        canvas.SetActive(false);
        // Debug.Log("deactivated2");
    }

    // Button for playing again (end menu -> game screen)
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Button for return to main menu (end menu -> start menu)
    public void ToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Button for exiting game
    public void ExitGame()
    {
        Application.Quit();
    }

    /*private void ActivateTimer(float duration, bool screen)
    {
        startScreen= screen;
        timerDuration = duration;
        timerTime = 0;
        timerActive = true;
    }*/
}
