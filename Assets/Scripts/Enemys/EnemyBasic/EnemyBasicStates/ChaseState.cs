using UnityEngine;
using UnityEngine.AI;

public class ChaseState<T> : State<T>
{
    Transform target;
    EnemyBasic _owner;
    Animator anim;
    NavMeshAgent ag;

    public ChaseState(EnemyBasic enemyBasic, Transform target, Animator anim, NavMeshAgent ag)
    {
        _owner = enemyBasic;
        this.anim = anim;
        this.target = target;
        this.ag = ag;
    }

    public override void Enter()
    {
        Debug.Log("PURSUE STATE");
    }

    public override void Update()
    {
        if (_owner.targetDetected)
        {
            anim.SetBool("IsWalking", true);
            ag.SetDestination(target.position);

            if (_owner.isCollisioning == true)
                _owner.m_SM.Feed(EnemyBasic.BE_Inputs.IsNear);

            if (_owner.isGettingDamage == true)
                _owner.m_SM.Feed(EnemyBasic.BE_Inputs.TakingDamage);
        }
        else
            _owner.m_SM.Feed(EnemyBasic.BE_Inputs.IsNotInSigth);
    }

    public override void Exit()
    {
        
    }
}
