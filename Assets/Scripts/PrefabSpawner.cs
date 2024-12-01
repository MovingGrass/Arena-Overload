using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private float yPosition = 0f;
    
    [Header("Player References")]
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float spawnMargin = 2f; // Minimum distance from players
    
    [Header("UI References")]
    [SerializeField] private Image timerFillImage;
    
    private float currentTime;
    private GameObject currentInstance;
    private Coroutine spawnRoutine;
    
    private void Start()
    {
        // Start the spawn check routine
        StartSpawnTimer();
    }
    
    private void StartSpawnTimer()
    {
        // Stop any existing routine
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
        
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Only start timer if there's no instance
            if (currentInstance == null)
            {
                // Reset timer
                currentTime = 0f;
                
                // Update timer until spawn
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
                
                // Spawn new instance
                SpawnPrefab();
            }
            else
            {
                // If there's an instance, reset the fill image
                if (timerFillImage != null)
                {
                    timerFillImage.fillAmount = 0f;
                }
            }
            
            yield return null;
        }
    }
    
    private void SpawnPrefab()
    {
        if (player1 == null || player2 == null)
        {
            Debug.LogError("Players not assigned!");
            return;
        }

        // Calculate spawn area between players
        Vector3 player1Pos = player1.position;
        Vector3 player2Pos = player2.position;
        
        // Find the midpoint between players
        Vector3 midPoint = (player1Pos + player2Pos) / 2f;
        
        // Calculate the direction from player1 to player2
        Vector3 playerDirection = (player2Pos - player1Pos).normalized;
        
        // Calculate perpendicular direction (for width of spawn area)
        Vector3 perpendicularDirection = Vector3.Cross(playerDirection, Vector3.up);
        
        // Calculate random position between players with some randomness to the sides
        float distanceBetweenPlayers = Vector3.Distance(player1Pos, player2Pos);
        float randomDistance = Random.Range(-distanceBetweenPlayers / 2f + spawnMargin, 
                                          distanceBetweenPlayers / 2f - spawnMargin);
        float randomWidth = Random.Range(-distanceBetweenPlayers / 4f, 
                                        distanceBetweenPlayers / 4f);
        
        Vector3 spawnPosition = midPoint + 
                              (playerDirection * randomDistance) + 
                              (perpendicularDirection * randomWidth);
        
        // Set the y position
        spawnPosition.y = yPosition;
        
        // Instantiate the prefab
        if (prefabToSpawn != null)
        {
            currentInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            
            // Add a listener to detect when the instance is destroyed
            DestroyListener destroyListener = currentInstance.AddComponent<DestroyListener>();
            destroyListener.OnDestroyEvent += OnInstanceDestroyed;
        }
        else
        {
            Debug.LogWarning("No prefab assigned to spawn!");
        }
    }
    
    private void OnInstanceDestroyed()
    {
        currentInstance = null;
        // Reset and start the timer again
        StartSpawnTimer();
    }
}

// Helper component to detect when an instance is destroyed
public class DestroyListener : MonoBehaviour
{
    public System.Action OnDestroyEvent;
    
    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }
}