
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Group
{
    public int enemyIndex;
    public int enemyCount;
    public int enemySpawnDelay;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] private float groupSpawnDelay;
    [SerializeField] private float offsetRadius;

    public Group currentGroup;
    public bool groupFinished { get; set; }
    public int groupNumber { get; set; }
    

    private bool spawning = true;

    private void Start()
    {
        groupFinished = true;
        groupNumber = 1;
    }

    private void Update()
    {
        // Spawn
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (!spawning || enemyPrefabs.Count == 0 || groupFinished) return;

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        int enemyPrefabIndex = currentGroup.enemyIndex;
        float spawnDelay = currentGroup.enemySpawnDelay;
        int groupSize = currentGroup.enemyCount;

        Vector2 offset = Random.insideUnitCircle * Random.Range(0, offsetRadius);

        Vector3 instantiationPosition = transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject enemy = Instantiate(enemyPrefabs[enemyPrefabIndex], instantiationPosition, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SpawnOffset = offset;

        groupSize--;

        currentGroup.enemyCount = groupSize;
        //enemyGroupSize.Peek = groupSize;÷

        spawning = false;
        if (groupSize == 0)
        {
            groupFinished = true;
            yield return new WaitForSeconds(groupSpawnDelay);
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        spawning = true;
    }

    public void SetGroup(Group group)
    {
        currentGroup = group;
        groupFinished = false;
        groupNumber++;
        Debug.Log("Added");
    }
}
