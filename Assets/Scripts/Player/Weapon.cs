using System;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    AssaultRifle,
    ShotGun
}

public class Weapon : MonoBehaviour
{
    public event Action StartShootAnimation = delegate { };
    public event Action StopShootAnimation = delegate { };
    public event Action StartReloadAnimation = delegate { };
    public event Action EndReloadAnimation = delegate { };

    public WeaponType WeaponType = WeaponType.Pistol;
    [SerializeField] GameObject _bulletPrefab = null;
    [SerializeField] Transform _bulletSpawnPoint = null;
    [SerializeField] int _magazine = 10;
    [SerializeField] int _backPack = 10;

    [Header("Stats")]
    [SerializeField] int _damage = 10;
    [SerializeField] int _ammoCapacity = 10;
    [SerializeField] float _fireRate = 1f;
    [SerializeField] float _reloadTime = 3f;
    [SerializeField] float _maxReloadTime = 3f;

    public bool InfiniteBullets = false;

    IFighter<HitData, HitResult> _owner;
    public bool canShoot { get => _magazine > 0; }
    public bool isReloading = false;
    public bool shootPhase = false;
    public bool isFiring = false;

    public void StartShooting()
    {
        shootPhase = true;
        StartShootAnimation();
    }
    public void StopShooting()
    {
        shootPhase = false;
        StopShootAnimation();
    }

    public void SetOwner(IFighter<HitData, HitResult> owner)
    {
        _owner = owner;
    }

    /// <summary>
    /// Instanciamos la bala y reducimos la cantidad de balas en nuestra magazine. Al llegar a 0 recargamos.
    /// </summary>
    public void Shoot()
    {
        if (_magazine > 0)
        {
            if (!InfiniteBullets) _magazine--;

            Bullet bulletInstace = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.LookRotation(_bulletSpawnPoint.forward)).GetComponent<Bullet>();
            bulletInstace.Damage = _damage;
            bulletInstace.SetOwner(_owner);

            if (_magazine <= 0)
                OnStartReload();
        }
    }

    public void OnStartReload()
    {
        StopShootAnimation();
        if (InfiniteBullets || _backPack > 0)
        {
            isReloading = true;
            StartReloadAnimation();
        }
    }
    public void OnEndReloading()
    {
        int bulletsToAdd = _ammoCapacity - _magazine; //si tengo 1 sola bala, necesito 9.
        if (!InfiniteBullets)
        {
            if (_backPack >= bulletsToAdd) _backPack -= bulletsToAdd;
            if (_backPack > 0 && _backPack < bulletsToAdd)
            {
                bulletsToAdd = _backPack;
                _backPack = 0;
            }
        }

        EndReloadAnimation();
        isReloading = false;
        _magazine = bulletsToAdd;
        print("====================== END RELOAD PHASE ============================");

        if (shootPhase) StartShootAnimation();
    }
    public void IncreaseDamage(int ammount)
    {
        if (ammount > 0)
            _damage += ammount;
    }
    public void IncreaseAmmoCapacity(int ammount)
    {
        if (ammount > 0)
            _ammoCapacity += ammount;
    }
    public void ReduxReloadTime(float Percentage)
    {
        if (Percentage > 0)
            _reloadTime -= _maxReloadTime * Percentage;
    }
    public void ReduxFireRate(float ammount)
    {
        if (ammount > 0)
            _fireRate -= ammount;
    }
}
