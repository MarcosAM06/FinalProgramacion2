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

public class Game : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadScene(sceneIndex sceneIndex)
    {
        SceneManager.LoadScene((int)sceneIndex);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
