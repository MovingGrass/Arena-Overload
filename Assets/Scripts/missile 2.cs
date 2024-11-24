using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Missile Properties")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float rotationSpeed = 95f;
    [SerializeField] private float maxLifetime = 5f;

    [SerializeField] private float bulletDamage = 10f;
    
    private Transform playerTarget;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Find player target immediately on instantiation
        playerTarget = GameObject.FindGameObjectWithTag("Player1")?.transform;
        
        if (playerTarget == null)
        {
            Debug.LogWarning("No player target found for homing missile!");
            Destroy(gameObject);
            return;
        }
        
        // Initial forward momentum
        rb.velocity = transform.forward * speed;
        
        // Set missile lifetime
        Destroy(gameObject, maxLifetime);
    }

    private void FixedUpdate()
    {
        if (playerTarget == null) return;
        
        // Calculate direction to player
        Vector3 direction = playerTarget.position - transform.position;
        
        // Calculate rotation to face player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        // Smoothly rotate towards player
        rb.MoveRotation(Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        ));
        
        // Update velocity to maintain constant speed in forward direction
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle impact effects here (explosion, damage, etc)
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
        Destroy(gameObject);
    }
}

