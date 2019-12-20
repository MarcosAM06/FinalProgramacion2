using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Action OnBossHasBeenKilled = delegate { };

    public int MaxLife;

    public float WaitTime = 3f;
    public bool isAttacking = false;

    [Header("Stun")]
    public bool stunned = false;
    public float StunTime = 4f;
    public float damageIncrease = 10;

    [Header("On Dead")]
    [SerializeField] Collider HurtBox = null;
    [SerializeField] Collider MainCollider = null;
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;


    public enum BS_Inputs
    {
        IsInSigth,
        IsNotInSigth,
        IsNear,
        IsNotNear,
        stunned,
        IsDead
    }

    public FSM<BS_Inputs> Sm;
    BS_Inputs evaluation = BS_Inputs.IsNotNear;

    protected override void Awake()
    {
        base.Awake();

        //State Machine
        var idle = new BS_Idle<BS_Inputs>(this, _anims);
        var chase = new BS_Chase<BS_Inputs>(this,_anims);
        var attack = new BS_Attack<BS_Inputs>(this, _anims);
        var stunned = new BS_Stunned<BS_Inputs>(this, _anims, StunTime);
        var Dead = new BS_Dead<BS_Inputs>(this, _anims);

        idle.AddTransition(BS_Inputs.IsDead, Dead)
            .AddTransition(BS_Inputs.stunned, stunned)
            .AddTransition(BS_Inputs.IsInSigth, chase);

        chase.AddTransition(BS_Inputs.IsDead, Dead)
             .AddTransition(BS_Inputs.IsNear, attack)
             .AddTransition(BS_Inputs.stunned, stunned)
             .AddTransition(BS_Inputs.IsNotInSigth, idle);

        attack.AddTransition(BS_Inputs.IsDead, Dead)
              .AddTransition(BS_Inputs.IsNotNear, chase)
              .AddTransition(BS_Inputs.stunned, stunned)
              .AddTransition(BS_Inputs.IsNotInSigth, idle);

        stunned.AddTransition(BS_Inputs.IsDead, Dead)
               .AddTransition(BS_Inputs.IsNotNear, chase)
               .AddTransition(BS_Inputs.IsNear, attack)
               .AddTransition(BS_Inputs.IsNotInSigth, idle);

        Sm = new FSM<BS_Inputs>(idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (_life <= 0)
        {
            Sm.Feed(BS_Inputs.IsDead);
            
        }
       

        base.Update();
        Sm.Update();

        if (IsInSight(_target))
            Sm.Feed(BS_Inputs.IsInSigth);
    }

    public override void DisableEntity()
    {
        OnBossHasBeenKilled();
        
            
        HurtBox.enabled = false;
        MainCollider.enabled = false;
        enabled = false;
    }

    //=================================== MEMBER FUNCS ===========================================

    public Transform GetTarget()
    {
        if (_target)
            return _target;
        else return null;
    }
    public override void EvaluateTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);
        if (distanceToTarget > AttackRange && IsInSight(_target)) //Condición de salida del ataque.
        {
            _anims.SetBool("BasicAttack", false);
            evaluation = BS_Inputs.IsNotNear;
        }
    }
    public override void ExecuteEvaluateAction()
    {
        if (evaluation == BS_Inputs.IsNotNear)
            Sm.Feed(evaluation);
    }
    //=================================== Sistema de Daño ========================================

    public override HitResult Hit(HitData hitData)
    {
        HitResult result = new HitResult();

        if (hitData.Damage > 0)
        {
            _life -= hitData.Damage;
            result.Conected = true;

            if (_life <= 0)
            {
                Destroy(doorLeft);
                Destroy(doorRight);
                //Matamos al Jefe.
                result.targetEliminated = true;
                GameProgressTracker.BossEnemyKilled();
                Sm.Feed(BS_Inputs.IsDead);
            }
        }

        return result;
        //Este boss es imparable asi que aunque reciba daño seguira su patrón de forma fiel.
    }

    public override HitData GetCombatStats()
    {
        return new HitData()
        {
            Damage = _damage
        };
    }
}
