using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform shootingPoint1;
    public float fireRate = 0.2f;  // Time between shots
    private float nextFireTime = 0f;

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
