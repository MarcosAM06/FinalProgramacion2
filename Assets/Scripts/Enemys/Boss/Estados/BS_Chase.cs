using UnityEngine;

public class BS_Chase<T> : State<T>
{
    Transform target;
    Boss _owner;
    Animator anim;

    public BS_Chase(Boss owner, Animator anim)
    {
        this.anim = anim;
        _owner = owner;
    }

    public override void Enter()
    {
        anim.SetBool("Walking", true);
        _owner.LookTowardsPlayer = true;
        target = _owner.GetTarget();
    }

    public override void Update()
    {
        if (target)
        {
            _owner.SetNavmeshDestination(target.position);

            float distToTarget = Vector3.Distance(_owner.transform.position, target.position);

            if (distToTarget <= _owner.AttackRange)
                _owner.Sm.Feed(Boss.BS_Inputs.IsNear);
        }
        else
            _owner.Sm.Feed(Boss.BS_Inputs.IsNotInSigth);
    }
}
