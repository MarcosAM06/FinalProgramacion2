using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState<T> : State<T>
{

    private Transform transform;
    private Transform debugTarget;
    public EnemyBasic Enemy;
    public Animator Enemybasic;

    public float SpeedChasing;
    public float DistanceForMaxSpeed;


    public ChaseState(Transform transform, Transform debugTarget, float SpeedChasing, float DistanceForMaxSpeed, EnemyBasic enemyBasic, Animator enemyBasicAnim)
    {
        this.Enemybasic = enemyBasicAnim;
        this.transform = transform;
        this.debugTarget = debugTarget;
        this.SpeedChasing = SpeedChasing;
        this.DistanceForMaxSpeed = DistanceForMaxSpeed;
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
            var distancia = Vector3.Distance(debugTarget.position, transform.position);
            var direccion = debugTarget.position - transform.position;
            this.transform.forward = direccion + this.transform.forward * Time.deltaTime;
            direccion.Normalize();

            if (distancia >= DistanceForMaxSpeed)

            {
                transform.position += direccion * SpeedChasing * Time.deltaTime;
            }

            else
            {
                transform.position += direccion * SpeedChasing * distancia / DistanceForMaxSpeed * Time.deltaTime;
            }


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
