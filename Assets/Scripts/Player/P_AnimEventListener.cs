using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AnimEventListener : MonoBehaviour
{
    Player _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<Player>();
    }

    void StartShoot()
    {
        _owner.CurrentWeapon.isFiring = true;
    }
    void EndShoot()
    {
        _owner.CurrentWeapon.isFiring = false;
    }
    void EventShoot()
    {
        _owner.CurrentWeapon.Shoot();
    }
}
