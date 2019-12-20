using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Heal : MonoBehaviour
{
    [SerializeField] int _healAmmount = 20;
    [SerializeField] float _lifeTime = 15f;
    [SerializeField] bool permanent = false;

    private void Update()
    {
        if (permanent) return;

        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.Health += _healAmmount;

            Destroy(gameObject);
        }
    }
}
