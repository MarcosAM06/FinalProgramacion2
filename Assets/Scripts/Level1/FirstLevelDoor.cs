using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelDoor : MonoBehaviour
{

    [SerializeField] Light Ligths;
    [SerializeField] Light Ligths1;
    [SerializeField] Keycard keycard;
    [SerializeField] Keycard keycard1;
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;

    public bool open;
    public bool open2;
    public Color StartColor;
    public Color EndColor;
    public float speed;




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
        keycard.tellDoor += ChangeLightColor;
        keycard1.tellDoor += ChangeLightColor2;

    }

    public void abrirPuerta()
    {
        if (open == true && open2 == true)
        {

            doorLeft.transform.position = Vector3.Lerp(doorLeft.transform.position, new Vector3(6.46f,0,0), 1) * Time.deltaTime * speed;

           

        }
    }


}
