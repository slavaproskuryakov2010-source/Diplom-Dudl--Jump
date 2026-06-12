using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float minYDistance = 2f;
    [SerializeField] private float maxYDistance = 3f;
    [SerializeField] private float spawnWidth = 5f;
    [SerializeField] private float destroyBelow = 10f;
    [SerializeField] private int startPlatforms = 10;

    private float highestY;
    private float lastPlayerY;
    private List<GameObject> platforms = new List<GameObject>();
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        highestY = player.position.y;
        lastPlayerY = highestY;

        for (int i = 0; i < startPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player.position.y > lastPlayerY + 2f)
        {
            lastPlayerY = player.position.y;

            while (highestY < player.position.y + maxYDistance * 3)
            {
                SpawnPlatform();
            }

            RemoveOldPlatforms();
        }
    }

    void SpawnPlatform()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        highestY += Random.Range(minYDistance, maxYDistance);

        GameObject platform;

        if (pool.Count > 0)
        {
            platform = pool.Dequeue();
        }
        else
        {
            platform = Instantiate(platformPrefab);
        }

        platform.transform.position = new Vector3(randomX, highestY, 0);
        platform.SetActive(true);
        platforms.Add(platform);
    }

    void RemoveOldPlatforms()
    {
        float destroyY = player.position.y - destroyBelow;

        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < destroyY)
            {
                platforms[i].SetActive(false);
                pool.Enqueue(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}