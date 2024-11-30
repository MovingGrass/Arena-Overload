using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private float yPosition = 0f;
    
    [Header("Spawn Range")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;
    
    [Header("UI References")]
    [SerializeField] private Image timerFillImage;
    
    private float nextSpawnTime;
    private float currentTime;
    
    private void Start()
    {
        // Initialize the timer
        nextSpawnTime = spawnInterval;
        currentTime = 0f;
        
        // Start the spawn routine
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Reset timer
            currentTime = 0f;
            
            // Spawn the prefab
            SpawnPrefab();
            
            // Update timer until next spawn
            while (currentTime < spawnInterval)
            {
                currentTime += Time.deltaTime;
                
                // Update fill image
                if (timerFillImage != null)
                {
                    timerFillImage.fillAmount = 1f - (currentTime / spawnInterval);
                }
                
                yield return null;
            }
        }
    }
    
    private void SpawnPrefab()
    {
        // Generate random position
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);
        
        // Instantiate the prefab
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No prefab assigned to spawn!");
        }
    }
}