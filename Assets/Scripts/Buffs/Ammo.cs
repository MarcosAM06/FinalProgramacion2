using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ammo : MonoBehaviour
{
    [SerializeField] WeaponType bulletType = WeaponType.Pistol;
    [SerializeField] int Ammount = 15;
    [SerializeField] float _lifeTime = 15f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.AddBullets(bulletType, Ammount);

            Destroy(gameObject);
        }
    }
}

