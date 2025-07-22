using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLMenu : MonoBehaviour
{
    public string preScene;

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneChanger.SceneChange("Start Game");
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        SceneChanger.SceneChange("Level Selector");
    }

    public void Reset()
    {
	    Time.timeScale = 1f;
        GameManager.Instance.Reset();
    }
}
