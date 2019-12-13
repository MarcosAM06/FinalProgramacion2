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
        if (Enemy.onDamage ==true)
        {
            Enemybasic.SetBool("GetHit", true);
        }


        else if (Enemy.onDamage == false)
        {
            Enemybasic.SetBool("GetHit", false);
            Enemy.stateMachine.Feed(EnemyBasic.Feed.NoTakingDamage);

        }

    }

 
}
