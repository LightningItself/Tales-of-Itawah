
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<int> enemyIndex;
    [SerializeField] private List<int> enemyGroupSize;
    [SerializeField] private List<float> enemySpawnDelay;

    [SerializeField] private float groupSpawnDelay;
    [SerializeField] private float offsetRadius;

    private int currentGroup;
    private int totalGroups;
    

    private bool spawning = true;

    private void Start()
    {
        totalGroups = enemyIndex.Count;
        currentGroup = 0;
    }

    private void Update()
    {
        // Spawn
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (currentGroup >= totalGroups || !spawning || enemyPrefabs.Count == 0) return;

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        int enemyPrefabIndex = enemyIndex[currentGroup];
        float spawnDelay = enemySpawnDelay[currentGroup];
        int groupSize = enemyGroupSize[currentGroup];

        Vector2 offset = Random.insideUnitCircle * Random.Range(0, offsetRadius);

        Vector3 instantiationPosition = transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject enemy = Instantiate(enemyPrefabs[enemyPrefabIndex], instantiationPosition, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SpawnOffset = offset;

        groupSize--;

        enemyGroupSize[currentGroup] = groupSize;

        spawning = false;
        if (groupSize == 0)
        {
            currentGroup++;
            yield return new WaitForSeconds(groupSpawnDelay);
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        spawning = true;
    }

}
