using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private HealthPlayer2 healthPlayer2;

    private HealthPlayer1 healthPlayer1;

    [SerializeField] private float healAmount = 70f;
    
    void Awake()
    {
        healthPlayer2 = FindObjectOfType<HealthPlayer2>();
        healthPlayer1 = FindObjectOfType<HealthPlayer1>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            healthPlayer1.HealPlayer1(healAmount);
            StartCoroutine(DestroySequence());
        }
        else if(other.CompareTag("Player2"))
        {
            healthPlayer2.HealPlayer2(healAmount);
            StartCoroutine(DestroySequence());
        }
    }

    IEnumerator DestroySequence()
    {
        // Disable the collider to prevent multiple triggers
        GetComponent<Collider>().enabled = false;

        // Hide the cube (you might want to replace this with your explosion animation)
        GetComponent<Renderer>().enabled = false;

        // Wait for the specified delay
        yield return new WaitForSeconds(0f);

        // Destroy the game object
        Destroy(gameObject); 
    }
}
