using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum SceneIndex : uint
{
    MainMenu = 0,
    Credits,
    Introduction,
    Lvl1,
    Lvl2,
    Lvl3,
    Victory,
    Defeat
}

public static class Game
{
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public static void LoadScene(SceneIndex sceneIndex)
    {
        SceneManager.LoadScene((int)sceneIndex);
    }
    public static void CloseGame()
    {
        Application.Quit();
    }
}
