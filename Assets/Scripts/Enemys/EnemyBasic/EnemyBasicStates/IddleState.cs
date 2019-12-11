using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleState<T> : State<T>
{

    public override void Enter()
    {
        Debug.Log("Enemigo iddle");
    }

}
