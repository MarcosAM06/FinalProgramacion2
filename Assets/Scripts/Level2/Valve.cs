using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Valve : MonoBehaviour
{
    public  Action tellDoor = delegate
    { };

    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            tellDoor();
            
        }

    }

}
