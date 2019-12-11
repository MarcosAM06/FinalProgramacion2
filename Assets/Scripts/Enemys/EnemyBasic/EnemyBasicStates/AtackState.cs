using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackState<T> : State<T>
{
    public EnemyBasic Enemy;
    public Animator Enemybasic;

    public AtackState(Animator enemyBasicAnim, EnemyBasic enemy)
    {
        this.Enemybasic = enemyBasicAnim;
        Enemy = enemy;
    }

    public override void Update()
    {

       

        if (Enemy.onCollision == false)
        {
            Enemy.stateMachine.Feed(EnemyBasic.Feed.IsNotNear);
        }

    }



}
