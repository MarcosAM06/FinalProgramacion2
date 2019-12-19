using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddsManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowSimpleAdd();
        }
    }

    public void ShowSimpleAdd()
    {
        //Primero tengo que chequar que el add esta listo.
        if (Advertisement.IsReady("video")) //Tambien puedo hacer Advertisement.IsReady("video") --> el string de parámetro corresponde a un placement (ver Adverticement en User Settings)
            Advertisement.Show("video"); //Advertisement.Show("Video"); --> siquiero ser mas específico.
    }

    public void ShowRewardAdd(string placement)
    {
        if (Advertisement.IsReady(placement))
        {
            var options = new ShowOptions { resultCallback = HandlerShowResult };
            Advertisement.Show(placement, options);
        }
    }

    private void HandlerShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                print("Fallido");
                break;
            case ShowResult.Skipped:
                print("Salteado");
                break;
            case ShowResult.Finished:
                print("Completado Exitosamente");
                break;
            default:
                break;
        }
    }

    //Acá ponemos los rewards.
}
