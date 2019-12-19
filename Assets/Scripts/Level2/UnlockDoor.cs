using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{

    [SerializeField] Light Ligths;
    [SerializeField] Light Ligths1;
    [SerializeField] Valve valve;
    [SerializeField] Valve valve1;
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;
    [SerializeField] GameObject vapor;
    [SerializeField] GameObject vapor1;


    public bool open;
    public bool open2;
    public Color StartColor;
    public Color EndColor;





    private void Update()
    {
        abrirPuerta();
    }

    public void ChangeLightColor()
    {
        var Luz = Ligths.GetComponent<Light>();
        Luz.color = Color.Lerp(StartColor, EndColor, 1);
        open = true;
    }

    public void ChangeLightColor2()
    {
        var Luz2 = Ligths1.GetComponent<Light>();
        Luz2.color = Color.Lerp(StartColor, EndColor, 1);
        open2 = true;
    }

    public void Awake()
    {
        valve.tellDoor += ChangeLightColor;
        valve1.tellDoor += ChangeLightColor2;

    }

    public void abrirPuerta()
    {
        if (open == true && open2 == true)
        {
            Destroy(doorLeft);
            Destroy(doorRight);
            Destroy(vapor);
            Destroy(vapor1);

        }
    }
}
