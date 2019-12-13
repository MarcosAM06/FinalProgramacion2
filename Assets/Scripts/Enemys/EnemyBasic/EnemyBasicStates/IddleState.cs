using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleState<T> : State<T>
{

    public Animator Enemybasic;
    public EnemyBasic Enemy;



    public IddleState(EnemyBasic enemyBasic, Animator enemyBasicAnim)
    {
        this.Enemybasic = enemyBasicAnim;
        Enemy = enemyBasic;

    }


    public override void Enter()
    {
        Debug.Log("Enemigo iddle");
        Enemybasic.SetBool("IsWalking", false);
    }

    public override void Update()
    {
        if (Enemy.isGettingDamage == true)
            {
                Enemy.m_SM.Feed(EnemyBasic.BE_Inputs.TakingDamage);
            }
    }

      



}
