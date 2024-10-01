using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Set the bullet's velocity
        rb.velocity = transform.forward * bulletSpeed;

        Destroy(gameObject, 1f);
    }

    // Optional: Destroy the bullet after some time or on collision
    void OnCollisionEnter(Collision collision)
    {
        // You might want to check what the bullet hit before destroying it
        Destroy(gameObject);
    }
}
