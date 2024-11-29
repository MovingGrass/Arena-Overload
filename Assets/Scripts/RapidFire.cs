using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class RapidFire : MonoBehaviour, IPowerUp
{
    public GameObject bulletPrefab;
    public Transform shootingPoint1;
    public float fireRate = 0.05f;
    private float nextFireTime = 0f;

    public float Duration => 10f; // Set the duration for this power-up

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