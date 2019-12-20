using UnityEngine;

public class BS_Attack<T> : State<T>
{
    Boss _owner;
    Animator Anims;

    public BS_Attack( Boss owner, Animator Anims )
    {
        _owner = owner;
        this.Anims = Anims;
    }

    public override void Enter()
    {
        Anims.SetBool("BasicAttack", true);
        _owner.isAttacking = true;
        _owner.StopNavmeshNavigation();
    }
    public override void Exit()
    {
        _owner.isAttacking = false;
    }

}
