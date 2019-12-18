using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHardDamageState<T> : State<T>
{
    public Animator Enemyhard;
    public EnemyHard Enemy;

    public TakeHardDamageState(EnemyHard enemyHard, Animator enemyHardAnim)
    {
        this.Enemyhard = enemyHardAnim;
        Enemy = enemyHard;
    }

    public override void Enter()
    {
        //Debug.Log("Enemigo recibe daño");
    }

    public override void Update()
    {
        if (Enemy.isGettingDamage == true)
        {
            Enemyhard.SetBool("GetHit", true);
        }

        else if (Enemy.isGettingDamage == false)
        {
            Enemyhard.SetBool("GetHit", false);
            Enemy.m_SM.Feed(EnemyHard.BE2_Inputs.NoTakingDamage);
        }
    }
}
