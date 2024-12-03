using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing2: MonoBehaviour, IPowerUp
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform shootingPoint2;
    public float fireRate = 0.2f;
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
        Quaternion rotation = shootingPoint2.rotation * Quaternion.Euler(0f, 0f, 0f);
        Instantiate(bulletPrefab, shootingPoint2.position, rotation);
    }
}
