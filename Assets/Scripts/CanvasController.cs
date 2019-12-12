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
