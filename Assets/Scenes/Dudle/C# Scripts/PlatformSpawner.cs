using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platfornPrefab;

    private void Start()
    {
        Vector3 SpawnerPos = new Vector3();

        for (int i = 0; i < 10; i++)
        {
            SpawnerPos.x = Random.Range(-2, 2);
            SpawnerPos.y += Random.Range(1f, 1.5f);

            Instantiate(platfornPrefab, SpawnerPos, Quaternion.identity);
        }
    }
}
