using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageState<T> : State<T>
{
    public Animator Enemybasic;
    public EnemyBasic Enemy;



    public TakeDamageState(EnemyBasic enemyBasic, Animator enemyBasicAnim)
    {
        this.Enemybasic = enemyBasicAnim;
        Enemy = enemyBasic;

    }


    public override void Enter()
    {
        Debug.Log("Enemigo recibe daño");
      
    }

    public override void Update()
    {
        if (Enemy.isGettingDamage ==true)
        {
            Enemybasic.SetBool("GetHit", true);
        }


        else if (Enemy.isGettingDamage == false)
        {
            Enemybasic.SetBool("GetHit", false);
            Enemy.m_SM.Feed(EnemyBasic.BE_Inputs.NoTakingDamage);

        }

    }

 
}
