using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Enemy : MonoBehaviour, IFighter<HitData, HitResult>
{
    //Stats Básicas
    [SerializeField] protected float life = 0;

    [Header("Line Of Sight")]
    [SerializeField] protected float range = 0;
    [SerializeField] protected float angle = 0;
    [SerializeField] protected LayerMask visibles = ~0;

    //Componentes de Unity.
    protected Transform _target;
    protected Animator _anims;
    protected NavMeshAgent _agent;

    //=================================== Unity Funcs ============================================

    protected virtual void Awake()
    {
        //Componentes
        _target = FindObjectOfType<Player>().transform;
        _anims = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    //=================================== Custom Funcs ===========================================

    /// <summary>
    /// Deshabilita esta unidad, pero no lo destruye del mundo.
    /// </summary>
    public virtual void DisableEntity() { enabled = false; }

    //=================================== Line of Sight ==========================================

    protected bool IsInSight(Transform target)
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

    //=================================== Sistema de Daño ========================================

    public bool IsAlive => life > 0;

    public virtual HitData GetCombatStats() { return new HitData() { Damage = 0 }; }

    public virtual HitResult Hit(HitData hitData)
    { return new HitResult() { Conected = false, targetEliminated = false }; }

    public virtual void OnHiConnected(HitResult hitResult) { }
}
