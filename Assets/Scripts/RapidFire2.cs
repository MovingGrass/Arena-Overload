using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire2 : MonoBehaviour, IPowerUp
{
   public GameObject bulletPrefab;
    public Transform shootingPoint2;
    public float fireRate = 0.05f;  // Time between shots
    private float nextFireTime = 0f;

    public float Duration => 10f;

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
        if (Input.GetKey(KeyCode.Return) && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint2.position, shootingPoint2.rotation);
    }
}
