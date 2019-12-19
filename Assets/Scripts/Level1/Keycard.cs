using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Keycard : MonoBehaviour
{
    public  Action tellDoor = delegate
    { };


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
          
            tellDoor();
            Destroy(gameObject);
        }

    }

  
   

}
