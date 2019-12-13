using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    public FSM<Feed> stateMachine;

    public float range;
    public float angle;
    public float life;
    public bool onSigth = true;
    public bool onCollision;
    public bool onDamage;
    public float SpeedChasing;
    public float DistanceForMaxSpeed;
    public LayerMask visibles = ~0;
    private Transform debugTarget;

    public Animator enemyBasicAnim;

    private void Awake()
    {
        debugTarget = FindObjectOfType<Player>().transform;
    }

    void Start()
    {
        enemyBasicAnim = GameObject.FindWithTag("EnemyBasic").GetComponent<Animator>();

        var Iddle = new IddleState<Feed>(this, enemyBasicAnim);
        var chase = new ChaseState<Feed>(this.transform, debugTarget, SpeedChasing, DistanceForMaxSpeed, this, enemyBasicAnim);
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
        if (c.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            onDamage = true;

        }


    }

    public void OnCollisionExit(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onCollision = false;
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            onDamage = false;

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
