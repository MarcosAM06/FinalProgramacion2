using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : MonoBehaviour, IFighter<HitData, HitResult>
{

    NavMeshAgent ag;
    public FSM<Feed> stateMachine;

    public float range;
    public float angle;
    public float life;
    public int EDamage;
    public bool onSigth = true;
    public bool onCollision;
    public bool onDamage;
    public LayerMask visibles = ~0;
    public Transform debugTarget;

    public Animator enemyBasicAnim;

    public bool IsAlive =>  life > 0;

    private void Awake()
    {
        debugTarget = FindObjectOfType<Player>().transform;
        ag = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        enemyBasicAnim = GetComponent<Animator>();

        var Iddle = new IddleState<Feed>(this, enemyBasicAnim);
        var chase = new ChaseState<Feed>(this.transform, debugTarget, this, enemyBasicAnim, ag);
        var atack = new AtackState<Feed>(this, enemyBasicAnim);
        var GetHit = new TakeDamageState<Feed>(this ,enemyBasicAnim);
        var Die = new DieState<Feed>();

        Iddle.AddTransition(Feed.IsInSigth, chase);
        chase.AddTransition(Feed.IsNotInSigth, Iddle);

        chase.AddTransition(Feed.IsNear, atack);
        atack.AddTransition(Feed.IsNotNear, chase);

        Iddle.AddTransition(Feed.TakingDamage, GetHit);
        chase.AddTransition(Feed.TakingDamage, GetHit);
        atack.AddTransition(Feed.TakingDamage, GetHit);

        GetHit.AddTransition(Feed.NoTakingDamage, Iddle);
        GetHit.AddTransition(Feed.IsDead, Die);


        stateMachine = new FSM<Feed>(Iddle);

    }

    void Update()
    {
        stateMachine.Update();
        onSigth = IsInSight(debugTarget);
        if (onSigth == true)
        {
            Debug.Log("la wea te ve");
            stateMachine.Feed(Feed.IsInSigth);
        }

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

    public void OnCollisionEnter(Collision c)
    {

        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onCollision = true;
        }

    }

    public void OnCollisionExit(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onCollision = false;
        }

    }

    void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(position, range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawLine(position, position + Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);

        if (debugTarget)
        Gizmos.DrawLine(position, debugTarget.position);
    }

    //EnemyBasic recibe daño
    public HitResult Hit(HitData hitData)
    {
        HitResult result = new HitResult();

        if (hitData.Damage > 0)
        {
            life -= hitData.Damage;

            result.Conected = true;
        }

        return result; 
    }

    //
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

    public enum Feed
    {
        IsInSigth,
        IsNotInSigth,
        IsNear,
        IsNotNear,
        TakingDamage,
        NoTakingDamage,
        IsDead

    }

}
