using UnityEngine;
using System.Collections;

public class PowerUpCube : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float bobSpeed = 1f;
    public float bobHeight = 0.5f;
    public GameObject explosionEffectPrefab;
    public float destroyDelay = 1f;

    private Vector3 startPosition;
    private PowerUpManager powerUpManager;

    private PowerUpManager2 powerUpManager2;

    void Start()
    {
        startPosition = transform.position;
        powerUpManager = FindObjectOfType<PowerUpManager>();
        powerUpManager2 = FindObjectOfType<PowerUpManager2>();
        
        if (powerUpManager == null)
        {
            Debug.LogError("PowerUpManager not found in the scene!");
        }
    }

    void Update()
    {
        // Rotate the cube
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob the cube up and down
        float newY = startPosition.y + (Mathf.Sin(Time.time * bobSpeed) * bobHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            // Trigger power-up effect
            if (powerUpManager != null)
            {
                powerUpManager.ActivatePowerUp();
            }

            // Play explosion effect
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }

            // Start destroy sequence
            StartCoroutine(DestroySequence());
        }
        else if(other.CompareTag("Player2"))
        {
            if (powerUpManager2 != null)
            {
                powerUpManager2.ActivatePowerUp();
            }

            // Play explosion effect
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }
            StartCoroutine(DestroySequence());
        }
    }

    IEnumerator DestroySequence()
    {
        // Disable the collider to prevent multiple triggers
        GetComponent<Collider>().enabled = false;

        // Hide the cube (you might want to replace this with your explosion animation)

        // Wait for the specified delay
        yield return new WaitForSeconds(destroyDelay);

        // Destroy the game object
        Destroy(gameObject); 
    }
}
