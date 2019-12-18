using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHard : MonoBehaviour, IFighter<HitData, HitResult>
{
    public bool targetDetected = true;
    public bool isGettingDamage = false;

    [SerializeField] float range = 0;
    [SerializeField] float angle = 0;
    [SerializeField] float life = 0;
    [SerializeField] int EDamage = 0;
    [SerializeField] LayerMask visibles = ~0;

    public enum BE2_Inputs
{
    IsInSigth,
    IsNotInSigth,
    TakingDamage,
    NoTakingDamage,
    IsDead
}


    public FSM<BE2_Inputs> m_SM;
    Transform _target;
    Animator _anims;
 

    public bool IsAlive => life > 0;

    private void Awake()
    {
        //Componentes
        _target = FindObjectOfType<Player>().transform;
        _anims = GetComponent<Animator>();

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

    public bool IsInSight(Transform target)
    {
        var positionDiference = target.position - transform.position;
        var distance = positionDiference.magnitude;
        var angleToTarget = Vector3.Angle(transform.forward, positionDiference);

        if (distance < range && angleToTarget < (angle / 2))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + Vector3.up, positionDiference.normalized, out hitInfo, range, visibles))
                return hitInfo.transform == target;
        }

        return false;
    }

    public void DisableEntity()
    {
       
        //Destroy(gameObject, 5f);

        this.enabled = false;
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


    public HitResult Hit(HitData hitData)
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

    public HitData GetCombatStats()
    {
        return new HitData()
        {
            Damage = EDamage
        };
    }

    public void OnHiConnected(HitResult hitResult)
    {
       
    }
}
