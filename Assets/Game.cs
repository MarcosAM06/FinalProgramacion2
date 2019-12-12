using UnityEngine;

using UnityEngine.SceneManagement;


public enum sceneIndex : uint
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
    public static void LoadScene(sceneIndex sceneIndex)
    {
        SceneManager.LoadScene((int)sceneIndex);
    }
    public static void CloseGame()
    {
        Application.Quit();
    }

    public void CheckButons()
    {
        
            CC.ActivateOneCanvas("PauseMenu");
        
    }



}
