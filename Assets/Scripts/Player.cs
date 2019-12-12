using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerHUD))]
public class Player : MonoBehaviour, IFighter<HitData,HitResult>
{
    [Header("Inspector")]
    [SerializeField] FixedJoystick _joystick = null;
    [SerializeField] Transform _worldForward = null;
    

    [Header("Enviroment Events")]
    [SerializeField] Actibable _currentActivable = null;
    [SerializeField] LayerMask _targeteables = 0;
    [SerializeField] float _targetDetectionRange = 10f;
    [SerializeField] IFighter<HitData, HitResult> _target = null;

    [Header("Stats")]
    [SerializeField] int _health = 100;
    [SerializeField] int _maxHealth = 100;

    //mi codigo:V
    [SerializeField]Animator PjAnim;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            _hud.SetHealthDisplay(value);
        }
    }
    [SerializeField] float _movementSpeed = 5f;

    [Header("Equipment")]
    public Weapon CurrentWeapon = null;
    [SerializeField] Dictionary<WeaponType, Weapon> Weapons = new Dictionary<WeaponType, Weapon>();

    [Header("Snap to Ground")]
    [SerializeField] LayerMask _groundLayer = 0;
    [SerializeField] float _upOffset = 1f;

    PlayerHUD _hud = null;
    Rigidbody _rb = null;
    Vector3 _axisDirection = Vector3.zero;
    Vector3 _movementDirection = Vector3.zero;
    Vector3 _raycastOrigin = Vector3.zero;
    Vector3 hitPoint = Vector3.zero;

    public bool IsAlive => _health > 0;

    private void Awake()
    {
        _hud = GetComponent<PlayerHUD>();
        _hud.SwitchToShootButton();
        Health = _maxHealth;
        _rb = GetComponent<Rigidbody>();

        var AviableWeapons = GetComponentsInChildren<Weapon>();
        Weapons = new Dictionary<WeaponType, Weapon>();
        foreach (var weaponComp in AviableWeapons)
            Weapons.Add(weaponComp.WeaponType, weaponComp);
        CurrentWeapon = Weapons[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        PjAnim = GetComponentInChildren<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPoint, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_raycastOrigin, 0.1f);
        Gizmos.matrix = Matrix4x4.Scale(new Vector3(1, 0, 1));
        Gizmos.DrawWireSphere(transform.position, _targetDetectionRange);
    }


    // Update is called once per frame
    void Update()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            MovePlayer(_joystick.Horizontal, _joystick.Vertical);
        else StopPlayerMovement();
        RotatePlayer();
        GetCloserTarget();
    }

    public void RotatePlayer()
    {
        if (_axisDirection != Vector3.zero)
            transform.forward = _axisDirection;
    }

    /// <summary>
    /// Obtiene una referencia al objetivo mas cercano.
    /// </summary>
    public void GetCloserTarget()
    {
        Collider[] targeteables = Physics.OverlapSphere(transform.position, _targetDetectionRange, _targeteables, QueryTriggerInteraction.Ignore);
        var targets = targeteables.Where(coll => coll.GetComponent<IFighter<HitData, HitResult>>() != null)
                                  .Select(coll => coll.GetComponent<IFighter<HitData, HitResult>>())
                                  .Where(Fighter => Fighter.gameObject != gameObject)
                                  .OrderBy(OtherFitghter => Vector3.Distance(transform.position, OtherFitghter.transform.position));

        if (targets.Any())
            _target = targets.First();
    }

    public void MovePlayer(float HAxis, float VAxis)
    {

        //Dirección de la cámara.
        PjAnim.SetBool("IsMoving", true);
        _axisDirection = _worldForward.forward * VAxis + _worldForward.right * HAxis;
        _movementDirection = _axisDirection;

        _raycastOrigin = transform.position + _axisDirection + new Vector3(0, _upOffset, 0);
        Ray ray = new Ray(_raycastOrigin, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, _groundLayer))
            _movementDirection = (hit.point - transform.position).normalized;
        hitPoint = hit.point;

        _rb.velocity = _movementDirection * _movementSpeed;
    }

    public void StopPlayerMovement()
    {
        PjAnim.SetBool("IsMoving", false);
        _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, 0.1f);
    }

    //Hoockeamos esto x UI.
    public void Shoot()
    {
        
    }

    //================================================ BUFFS ==============================================================================

    public void AddBullets(WeaponType weapon, int bulletAmmounts)
    {
        //Acá va el código para añadir balas segun el arma.
    }
    public Weapon GetWeaponByType(WeaponType weaponType)
    {
        if (Weapons.ContainsKey(weaponType))
            return Weapons[weaponType];

        return null;
    }

    //================================================ COLLISIONES ========================================================================

    //Interacción con objetos.
    private void OnTriggerEnter(Collider other)
    {
        //Obtenemos una referencia a un objeto interactuable.
        print("ENTRE EN EL TRIGGER");
        _currentActivable = other.GetComponent<Actibable>();
        if (_currentActivable)
        {
            _hud.SwitchToInteractButton();
            other.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print("SALGO DEL TRIGGER");
        if (_currentActivable)
        {
            _currentActivable = null;
            _hud.SwitchToShootButton();
            other.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    //================================================ SISTEMA DE DAÑO ====================================================================

    public HitResult Hit(HitData hitData)
    {
        HitResult result = new HitResult();

        // Es mas flexible de este modo porque puedo agregar modificadores de Daño para los atques salientes
        // y Tambien resistencias para el daño entrante.
        if (hitData.Damage > 0)
        {
            Health -= hitData.Damage;

            result.Conected = true;
        }

        return result;
    }

    /// <summary>
    /// Permite obtener información detallada del resultado de un Hit provocado a una segunda unidad.
    /// </summary>
    /// <param name="hitResult">Resultado del Hit</param>
    public void OnHiConnected(HitResult hitResult)
    {
        //Puedo hacer cosas en base al resultado del hit.
    }

    /// <summary>
    /// Devuelve las estadísticas de combate de esta unidad. Modificadores de Daño.
    /// </summary>
    public HitData GetCombatStats()
    {
        return new HitData();
    }
}
