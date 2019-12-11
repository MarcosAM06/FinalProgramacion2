using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponUpgrade : MonoBehaviour
{
    [Header("General")]
    [SerializeField] WeaponType weaponType = WeaponType.Pistol;
    [SerializeField] float _lifeTime = 15f;

    [Header("Buff")]
    [SerializeField] int _damageBoost = 0;
    [SerializeField] int _ammoCapacityBoost = 0;
    [SerializeField, Range(0f, 1f)] float _reloadSpeedBoost = 0f;
    [SerializeField, Range(0f, 0.5f)] float _fireRateReduction = 0f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        Weapon playerWeapon = player.GetWeaponByType(weaponType);
        if (player && playerWeapon)
        {
            playerWeapon.IncreaseDamage(_damageBoost);
            playerWeapon.IncreaseAmmoCapacity(_ammoCapacityBoost);
            playerWeapon.IncreaseReloadSpeed(_reloadSpeedBoost);
            playerWeapon.ReduxFireRate(_fireRateReduction);

            Destroy(gameObject);
        }
    }
}
