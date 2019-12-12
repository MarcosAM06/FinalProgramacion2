using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    static public CanvasController Instance { get; private set; }

    private GameObject PauseMenu;

    public static bool IsInPause;
    public List<GameObject> ListCanvasObj = new List<GameObject>();

	void Start ()
    {
        Instance = this;
        FindAllGAmeObjInCanvas();
    }
    void Update()
    {
       
    }

    void FindAllGAmeObjInCanvas()
    {
        PauseMenu = GameObject.Find("PauseMenu");
        ListCanvasObj.Add(PauseMenu);
        PauseMenu.SetActive(false);
    }

    public void ActivateOneCanvas(string CanvasName)
    {
        foreach (var item in ListCanvasObj)
        {
            if (item.name != CanvasName)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
                Time.timeScale = 0;
    }

    public void Continue()
    {
        ActivateOneCanvas("Panel");
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void X()
    {
        Application.Quit();
    }


}
