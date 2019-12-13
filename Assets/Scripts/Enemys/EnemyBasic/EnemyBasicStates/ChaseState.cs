using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState<T> : State<T>
{

    private Transform transform;
    private Transform debugTarget;
    public EnemyBasic Enemy;
    public Animator Enemybasic;
    public NavMeshAgent ag;



    public ChaseState(Transform transform, Transform debugTarget, EnemyBasic enemyBasic, Animator enemyBasicAnim, NavMeshAgent ag)
    {
        this.Enemybasic = enemyBasicAnim;
        this.transform = transform;
        this.debugTarget = debugTarget;
        Enemy = enemyBasic;
    }

    public override void Enter()
    {
        Debug.Log("Enemigo persiguiendo");
    }

    public override void Update()
    {
        if (Enemy.onSigth == true)
        {
            Enemybasic.SetBool("IsWalking", true);
            ag.SetDestination(debugTarget.position);



            if (Enemy.onCollision == true)
            {
                Enemy.stateMachine.Feed(EnemyBasic.Feed.IsNear);
            }

            if (Enemy.onDamage == true)
            {
                Enemy.stateMachine.Feed(EnemyBasic.Feed.TakingDamage);
            }

        }

        else
        {
            Enemy.stateMachine.Feed(EnemyBasic.Feed.IsNotInSigth);
        }

    }



}
