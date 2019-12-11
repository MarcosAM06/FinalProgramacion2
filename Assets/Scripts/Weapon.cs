using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    AssaultRifle,
    ShotGun
}

public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType = WeaponType.Pistol;
    [SerializeField] GameObject _bulletPrefab = null;
    [SerializeField] Transform _bulletSpawnPoint = null;
    [SerializeField] int _magazine = 10;
    [SerializeField] int _backPack = 10;

    [Header("Stats")]
    [SerializeField] int _damage = 10;
    [SerializeField] int _ammoCapacity = 10;
    [SerializeField, Range(0.2f,1f)] float _reloadSpeed = 0.2f;
    [SerializeField] float _fireRate = 1f;
    [SerializeField] float maxReloadTime = 3f;

    [SerializeField] bool infiniteBullets = false;

    IFighter<HitData, HitResult> _owner;
    bool reloading = false;
    float timeToShoot = 0;

    float ReloadTime { get => _reloadSpeed * maxReloadTime; }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOwner(IFighter<HitData, HitResult> owner)
    {
        _owner = owner;
    }

    public void Shoot()
    {
        if (reloading) return;

        if (_magazine > 0)
        {
            Bullet bulletInstace = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.LookRotation(_bulletSpawnPoint.forward)).GetComponent<Bullet>();
            bulletInstace.SetOwner(_owner);
        }
        else
            Reload();
    }
    public void Reload()
    {
        //ReloadShit;
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
    public void IncreaseReloadSpeed(float percentage)
    {
        if (percentage > 0)
            _reloadSpeed += maxReloadTime * percentage;
    }
    public void ReduxFireRate(float ammount)
    {
        if (ammount > 0)
            _fireRate -= ammount;
    }
}
