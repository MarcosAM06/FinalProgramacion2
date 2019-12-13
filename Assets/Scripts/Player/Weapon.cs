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

    [SerializeField] bool _infiniteBullets = false;

    IFighter<HitData, HitResult> _owner;
    public bool isReloading = false;
    public bool shootPhase = false;
    public bool isFiring = false;
    float timeToShoot = 0;

    // Update is called once per frame
    void Update()
    {
        //Reloading.
        if (timeToShoot > 0)
        {
            StopShootAnimation();

            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0 && isReloading)
            {
                isReloading = false;
                timeToShoot = 0;

                int bulletsToAdd = _ammoCapacity - _magazine; //si tengo 1 sola bala, necesito 9.
                if (_backPack >= bulletsToAdd)
                    _backPack -= bulletsToAdd;
                if (_backPack > 0 && _backPack < bulletsToAdd)
                {
                    bulletsToAdd = _backPack;
                    _backPack = 0;
                }

                _magazine = bulletsToAdd;
                print("====================== END RELOAD PHASE ============================");

                if (shootPhase) StartShootAnimation();
            }
        }

    }

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
            _magazine--;
            Bullet bulletInstace = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.LookRotation(_bulletSpawnPoint.forward)).GetComponent<Bullet>();
            bulletInstace.SetOwner(_owner);
        }
        else
            Reload();
    }
    public void Reload()
    {
        print("====================== START RELOAD PHASE ============================");
        if (!_infiniteBullets)
        {
            print("Las balas no son infinitas...");
            //Si las balas no son infinitas, solo cargamos si tengo suficientes balas en el backpack.
            if (_backPack > 0)
            {
                isReloading = true;
                timeToShoot = _reloadTime;
            }
            else
            {
                //NO tenemos balas para cargar.
            }
        }
        else
        {
            print("Las balas SON infinitas...");
            //Si las balas son infinitas, relodeamos igualmente.
            isReloading = true;
            timeToShoot = _reloadTime;
        }
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
