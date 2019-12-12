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

    public CanvasController CC;

    private void Start()
    {
        CC = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }

    void Update()
    {
        //Acessos rapidos para los niveles

        CheckButons();
    }


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

    void CheckButons()
    {


        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    SceneManager.LoadScene("Level1");
        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    SceneManager.LoadScene("Level2");
        //}

        //if (Input.GetKey(KeyCode.Alpha3))
        //{
        //    SceneManager.LoadScene("Level3");

        //}


        if (Input.GetKeyDown(KeyCode.P))
        {
            CC.ActivateOneCanvas("PauseMenu");
        }
    }
}
