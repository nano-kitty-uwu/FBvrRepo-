using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform player;
    public float spawnRadiusMin = 10f;
    public float spawnRadiusMax = 20f;
    public float spawnHeightMin = 1f;
    public float spawnHeightMax = 5f;
    public float spawnInterval = 2f;
    public float minSpeed = 3f; 
    public float maxSpeed = 8f;

    private float platformY;

    void Start()
    {
        platformY = player.position.y;
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        float angle = Random.Range(0f, 360f);
        Vector3 spawnPosition = new Vector3(
            player.position.x + Random.Range(spawnRadiusMin, spawnRadiusMax) * Mathf.Cos(angle),
            Random.Range(platformY + spawnHeightMin, platformY + spawnHeightMax),
            player.position.z + Random.Range(spawnRadiusMin, spawnRadiusMax) * Mathf.Sin(angle)
        );

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Set random speed
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        asteroid.AddComponent<AsteroidMovement>().Initialize(player.position, randomSpeed);
    }
}