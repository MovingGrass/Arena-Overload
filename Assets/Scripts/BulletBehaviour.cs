using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; // Tambahkan ini untuk VFX Graph

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float bulletDamage = 10f;
    private Rigidbody rb;

    // Referensi ke VFX Graph
    public GameObject hitVFXPrefab; // Drag prefab VFX ke sini di Inspector

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Spawn VFX di titik impact
        if (hitVFXPrefab != null)
        {
            // Dapatkan titik impact
            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;

            // Spawn dan play VFX
            GameObject vfx = Instantiate(hitVFXPrefab, position, Quaternion.LookRotation(contact.normal));
            Destroy(vfx, 2f); // Hancurkan VFX setelah 2 detik
        }

        if (collision.collider.CompareTag("Player1"))
        {
            HealthPlayer1 player1Health = collision.collider.GetComponent<HealthPlayer1>();
            if (player1Health != null)
            {
                player1Health.TakeDamagePlayer1(bulletDamage);
            }
        }
        else if (collision.collider.CompareTag("Player2"))
        {
            HealthPlayer2 player2Health = collision.collider.GetComponent<HealthPlayer2>();
            if (player2Health != null)
            {
                player2Health.TakeDamagePlayer2(bulletDamage);
            }
        }

        Destroy(gameObject);
    }
}