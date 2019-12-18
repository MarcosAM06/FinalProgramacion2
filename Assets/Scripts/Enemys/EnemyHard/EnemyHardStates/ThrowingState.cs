using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingState<T> : State<T>
{
    public EnemyHard Enemy;
    public Animator Enemyhard;


    public ThrowingState(EnemyHard enemy, Animator enemyHardAnim)
    {
        this.Enemyhard = enemyHardAnim;
        Enemy = enemy;
    }
}
