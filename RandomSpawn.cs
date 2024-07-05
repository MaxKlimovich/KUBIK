using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] cube;
    public List<Transform> spawnPoints;
    void Start()
    {
        spawnPoints = new List<Transform>(spawnPoints);
        SpawnCube();
    }

    private void SpawnCube()
    {
        for (int i = 0; i < cube.Length; i++)
        {
            var spawn = Random.Range(0, spawnPoints.Count);
            Instantiate(cube[i], spawnPoints[spawn].transform.position, Quaternion.identity);
            spawnPoints.RemoveAt(spawn);
        }
    }
}
