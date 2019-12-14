using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemyBasic : MonoBehaviour, IFighter<HitData, HitResult>
{
    public bool targetDetected = true;
    public bool isCollisioning = false;
    public bool isGettingDamage = false;

    [SerializeField] float range = 0;
    [SerializeField] float angle = 0;
    [SerializeField] float life = 0;
    [SerializeField] int EDamage = 0;
    [SerializeField] LayerMask visibles = ~0;
    [SerializeField] Collider HurtBox = null;
    [SerializeField] Collider HitBox = null;
    [SerializeField] Collider MainCollider = null;

    public enum BE_Inputs
    {
        IsInSigth,
        IsNotInSigth,
        IsNear,
        IsNotNear,
        TakingDamage,
        NoTakingDamage,
        IsDead
    }
    public FSM<BE_Inputs> m_SM;
    Transform _target;
    Animator _anims;
    NavMeshAgent _agent;

    public bool IsAlive =>  life > 0;

    private void Awake()
    {
        //Componentes
        _target = FindObjectOfType<Player>().transform;
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<Animator>();

        //State Machine.
        var Iddle = new IddleState<BE_Inputs>(this, _anims);
        var chase = new ChaseState<BE_Inputs>(this, _target, _anims, _agent);
        var atack = new AtackState<BE_Inputs>(this, _anims);
        var GetHit = new TakeDamageState<BE_Inputs>(this, _anims);
        var Die = new DieState<BE_Inputs>(this, _anims);

        Iddle.AddTransition(BE_Inputs.IsInSigth, chase);
        Iddle.AddTransition(BE_Inputs.IsDead, Die);
        chase.AddTransition(BE_Inputs.IsNotInSigth, Iddle);

        chase.AddTransition(BE_Inputs.IsNear, atack);
        atack.AddTransition(BE_Inputs.IsNotNear, chase);

        Iddle.AddTransition(BE_Inputs.TakingDamage, GetHit);
        chase.AddTransition(BE_Inputs.TakingDamage, GetHit);
        atack.AddTransition(BE_Inputs.TakingDamage, GetHit);

        GetHit.AddTransition(BE_Inputs.NoTakingDamage, Iddle);
        GetHit.AddTransition(BE_Inputs.IsDead, Die);

        m_SM = new FSM<BE_Inputs>(Iddle);
    }

    void Update()
    {
        m_SM.Update();

        targetDetected = IsInSight(_target);
        if (targetDetected == true)
            m_SM.Feed(BE_Inputs.IsInSigth);
    }

    //Line Of Sight
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
        HitBox.enabled = false;
        HurtBox.enabled = false;
        MainCollider.enabled = false;
        _agent.enabled = false;
        //Destroy(gameObject, 5f);

        this.enabled = false;
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isCollisioning = true;
        }
    }

    public void OnCollisionExit(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isCollisioning = false;
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

        if (_target)
        Gizmos.DrawLine(position, _target.position);
    }

    //EnemyBasic recibe daño
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
                m_SM.Feed(BE_Inputs.IsDead);
            }
            else
            {
                m_SM.Feed(BE_Inputs.TakingDamage);
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
       // Por ahora nada we.
    }
}
