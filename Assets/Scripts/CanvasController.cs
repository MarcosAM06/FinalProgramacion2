using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    static public CanvasController Instance { get; private set; }

    [SerializeField] GameObject PauseMenu;

    public static bool IsInPause;

	void Start ()
    {
        Instance = this;
       
    }
    void Update()
    {
       
    }
    //PANTALLA DE MENU
    public void StartGame()
    {
        Game.LoadScene(sceneIndex.Lvl1);
    }
    public void Credits()
    {
        Game.LoadScene(sceneIndex.Credits);
    }

    //MENU DE PAUSA
    public void Continue()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        Game.LoadScene(sceneIndex.MainMenu);
    }

    public void X()
    {
        Application.Quit();
    }
    public void ActivatePause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

}
