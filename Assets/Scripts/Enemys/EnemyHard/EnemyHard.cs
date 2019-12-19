using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHard : Enemy 
{
    public bool targetDetected = true;
    public bool isGettingDamage = false;

    [SerializeField] int EDamage = 0;

    public enum BE2_Inputs
    {
        IsInSigth,
        IsNotInSigth,
        TakingDamage,
        NoTakingDamage,
        IsDead
    }

    public FSM<BE2_Inputs> m_SM;

    protected override void Awake()
    {
        base.Awake(); //El Awake base rellena los componentes básicos.

        //State Machine.
        var Iddle = new IddleHardState<BE2_Inputs>(this, _anims);
        var Throw = new ThrowingState<BE2_Inputs>(this, _anims);
        var GetHit = new TakeHardDamageState<BE2_Inputs>(this, _anims);
        var Die = new DieHardState<BE2_Inputs>(this, _anims);

        Iddle.AddTransition(BE2_Inputs.IsInSigth, Throw);

        Throw.AddTransition(BE2_Inputs.IsNotInSigth, Iddle);

        Iddle.AddTransition(BE2_Inputs.TakingDamage, GetHit);

        Throw.AddTransition(BE2_Inputs.TakingDamage, GetHit);

        GetHit.AddTransition(BE2_Inputs.NoTakingDamage, Iddle);
        GetHit.AddTransition(BE2_Inputs.IsDead, Die);

        m_SM = new FSM<BE2_Inputs>(Iddle);
    }

    void Update()
    {
        m_SM.Update();

        targetDetected = IsInSight(_target);
        if (targetDetected == true)
            m_SM.Feed(BE2_Inputs.IsInSigth);
    }

    public override void DisableEntity()
    {
        //Destroy(gameObject, 5f);

        enabled = false;
    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position, range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

        if (_target)
            Gizmos.DrawLine(position, _target.position);
    }

    //=================================== Sistema de Daño ========================================

    public override HitResult Hit(HitData hitData)
    {
        HitResult result = new HitResult();

        if (hitData.Damage > 0)
        {
            life -= hitData.Damage;
            print("HIT");

            result.Conected = true;

            if (life <= 0)
            {
                result.targetEliminated = true;
                m_SM.Feed(BE2_Inputs.IsDead);
            }
            else
            {
                m_SM.Feed(BE2_Inputs.TakingDamage);
            }
        }

        return result;
    }

    public override HitData GetCombatStats()
    {
        return new HitData()
        {
            Damage = EDamage
        };
    }
}
