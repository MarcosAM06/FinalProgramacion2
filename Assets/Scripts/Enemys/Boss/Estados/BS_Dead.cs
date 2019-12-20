using UnityEngine;

public class BS_Dead<T> : State<T>
{
    Animator anims;
    Boss _owner;

    public BS_Dead( Boss owner, Animator anims)
    {
        _owner = owner;
        this.anims = anims;
    }

    public override void Enter()
    {
        anims.SetBool("Dead", true);
        anims.SetBool("BasicAttack", false);
        _owner.DisableEntity();
    }
}
