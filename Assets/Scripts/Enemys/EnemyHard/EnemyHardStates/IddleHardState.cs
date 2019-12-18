using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleHardState<T> : State<T>
{
    public Animator Enemyhard;
    public EnemyHard Enemy;

    public IddleHardState(EnemyHard enemyHard, Animator enemyHardAnim)
    {
        this.Enemyhard = enemyHardAnim;
        Enemy = enemyHard;
    }
}
