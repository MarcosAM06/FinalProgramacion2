using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    static public CanvasController Instance { get; private set; }

    [SerializeField] GameObject PauseMenu = null;
    [SerializeField] GameObject GameOptions = null;
    [SerializeField] GameObject FixedJoystick = null;
    [SerializeField] GameObject ShootButton = null;
    [SerializeField] GameObject InteractButton = null;


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
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        GameOptions.SetActive(true);
        FixedJoystick.SetActive(true);
        ShootButton.SetActive(true);
        InteractButton.SetActive(true);
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
        GameOptions.SetActive(false);
        FixedJoystick.SetActive(false);
        ShootButton.SetActive(false);
        InteractButton.SetActive(false);
    }
}
