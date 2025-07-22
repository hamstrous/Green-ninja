using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool pauseClick = false;
    public GameObject pauseMenuUI;
    
    void Start()
    {
        
    }

    public void PauseClick()
    {
        pauseClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseClick)
        {
            pauseClick = false;
            if (!GamePaused)
            {
                GamePaused = true;
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true); 
        GamePaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GamePaused = false;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GamePaused = false;
        SceneChanger.SceneChange("Start Game");
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GamePaused = false;
        SceneChanger.SceneChange("Level Selector");
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GamePaused = false;
        GameManager.Instance.Reset();
    }
}
