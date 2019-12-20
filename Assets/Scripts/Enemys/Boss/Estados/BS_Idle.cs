using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_Idle<T> : State<T>
{
    Animator anims;
    Boss _owner;

    public BS_Idle(Boss owner, Animator anims)
    {
        this.anims = anims;
        _owner = owner;
    }

    public override void Enter()
    {
        anims.SetBool("Walking", false);
    }
}
