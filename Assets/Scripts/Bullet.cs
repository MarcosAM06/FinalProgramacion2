using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float _travelSpeed = 10f;
    [SerializeField] float _maxLifeTime = 5f;

    float _lifeTime;

    Rigidbody _rb;
    IFighter<HitData, HitResult> _owner;

    private void Awake()
    {
        _lifeTime = _maxLifeTime;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(transform.forward * _travelSpeed, ForceMode.VelocityChange);

        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0) Destroy(gameObject);
    }

    public void SetOwner(IFighter<HitData, HitResult> owner)
    {
        _owner = owner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != _owner.gameObject)
        {
            //Aplica daño al target;

            Destroy(gameObject);
        }
    }
}
