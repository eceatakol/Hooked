using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public GameObject goldenFishPrefab;
    public bool goldenFishSpawned = false;
    public Transform boatTransform;
    public float spawnInterval = 2f;

    public float spawnY = -0.5f; // Slightly underwater

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFish();
            timer = 0f;
        }

        if (!goldenFishSpawned && ScoreManager.Instance != null && ScoreManager.Instance.score >= 15)
        {
            SpawnGoldenFishNearBoat();
            goldenFishSpawned = true;
        }
    }

    void SpawnFish()
    {
        int randomIndex = Random.Range(0, fishPrefabs.Length);

        Vector3 boatPosition = boatTransform.position;

        // Smaller spawn area around the boat
        float randomX = Random.Range(boatPosition.x - 3f, boatPosition.x + 3f);
        float randomZ = Random.Range(boatPosition.z - 3f, boatPosition.z + 3f);

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        Instantiate(fishPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
    
    void SpawnGoldenFishNearBoat()
    {
    GameObject boat = GameObject.Find("single boat");
    if (boat == null)
    {
        Debug.LogWarning("Boat not found!");
        return;
    }

    Vector3 boatPos = boat.transform.position;
    Vector3 spawnPos = boatPos + new Vector3(2f, -0.5f, 0f); // Offset

    Instantiate(goldenFishPrefab, spawnPos, Quaternion.identity);
    Debug.Log("✨ Golden Fish Spawned at: " + spawnPos);
    }
}