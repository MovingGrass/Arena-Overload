using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; // Tambahkan ini

public class HomingMissiles : MonoBehaviour
{
    [Header("Missile Properties")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float rotationSpeed = 95f;
    [SerializeField] private float maxLifetime = 5f;
    [SerializeField] private float bulletDamage = 10f;

    [Header("VFX")]
    [SerializeField] private GameObject hitVFXPrefab; // Referensi ke prefab VFX

    private Transform playerTarget;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindGameObjectWithTag("Player2")?.transform;

        if (playerTarget == null)
        {
            Debug.LogWarning("No player target found for homing missile!");
            Destroy(gameObject);
            return;
        }

        rb.velocity = transform.forward * speed;
        Destroy(gameObject, maxLifetime);
    }

    private void FixedUpdate()
    {
        if (playerTarget == null) return;

        Vector3 direction = playerTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        rb.MoveRotation(Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        ));

        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Spawn VFX saat terjadi impact
        if (hitVFXPrefab != null)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;

            // Spawn VFX di titik impact dengan rotasi menghadap normal permukaan
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