using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DefaultGun1 : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    public Transform shootingPoint1;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint1.position, shootingPoint1.rotation);
    }
}