using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void startGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}