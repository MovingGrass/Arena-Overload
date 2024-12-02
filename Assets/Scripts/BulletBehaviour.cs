using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float bulletDamage = 10f; // Amount of damage the bullet does
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Set the bullet's velocity
        rb.velocity = transform.forward * bulletSpeed;

        Destroy(gameObject, 5f); // Destroy the bullet after 1 second
    }

    // Detect collision
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object has the tag "Player1"
        if (collision.collider.CompareTag("Player1"))
        {
            // Try to get the HealthPlayer1 component from the object
            HealthPlayer1 player1Health = collision.collider.GetComponent<HealthPlayer1>();

            if (player1Health != null)
            {
                // Call TakeDamagePlayer1 method and pass in the damage
                player1Health.TakeDamagePlayer1(bulletDamage);
            }
        }
        // Check if the object has the tag "Player2"
        else if (collision.collider.CompareTag("Player2"))
        {
            // Try to get the HealthPlayer2 component from the object
            HealthPlayer2 player2Health = collision.collider.GetComponent<HealthPlayer2>();

            if (player2Health != null)
            {
                // Call TakeDamagePlayer1 method in HealthPlayer2 script and pass in the damage
                player2Health.TakeDamagePlayer2(bulletDamage);
            }
        }

        // Destroy the bullet upon collision
        Destroy(gameObject);
    }
}


