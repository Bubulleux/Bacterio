using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
