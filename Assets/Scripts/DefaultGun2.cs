using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform shootingPoint2;
    public float fireRate = 0.2f;  // Time between shots
    private float nextFireTime = 0f;

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
