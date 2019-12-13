using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState<T> : State<T>
{
    public EnemyBasic Enemy;
    public Animator Enemybasic;


    public AtackState( EnemyBasic enemy, Animator enemyBasicAnim)
    {
        this.Enemybasic = enemyBasicAnim;
        Enemy = enemy;
    }


    public override void Enter()
    {
        Debug.Log("Enemigo Atacando");

    }



    public override void Update()
    {
        if (Enemy.isCollisioning == false)
        {
            Enemybasic.SetBool("IsWalking", true);
            Enemybasic.SetBool("IsAtacking", false);
            Enemy.m_SM.Feed(EnemyBasic.BE_Inputs.IsNotNear);

        }
        else
        {
            Enemybasic.SetBool("IsAtacking", true);
        }

    }



}
