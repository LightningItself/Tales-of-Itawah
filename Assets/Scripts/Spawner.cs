
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
    [SerializeField] private float waveHealthIncrement = 3f;
    [SerializeField] private float waveAttackIncrement = 2f;
    [SerializeField] private float waveHealthIncrementAfter5 = 3f;
    [SerializeField] private float waveAttackIncrementAfter5 = 2f;
    [SerializeField] private GameManager gm;


    private float currentHealthInc = 0;
    private float currentDamageInc = 0;

    public Group currentGroup;
    public bool GroupFinished { get; set; }
    public int GroupNumber { get; set; }

    private bool canIncreseCost = true;

    public float groupNumber;

    private bool spawning = true;

    private void Start()
    {
        GroupFinished = true;
        GroupNumber = 1;
    }

    private void Update()
    {
        // Spawn
        SpawnEnemy();
        groupNumber = GroupNumber;
    }

    private void SpawnEnemy()
    {
        if (!spawning || enemyPrefabs.Count == 0 || GroupFinished) return;

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

        EnemyController ec = enemy.GetComponent<EnemyController>();
        ec.Attacker.AttackBoost = currentHealthInc;
        ec.Damagable.HealthBoost = currentDamageInc;
        
        ec.SpawnOffset = offset;

        groupSize--;

        currentGroup.enemyCount = groupSize;
        //enemyGroupSize.Peek = groupSize;÷

        spawning = false;
        if (groupSize <= 0)
        {
            GroupFinished = true;
            yield return new WaitForSeconds(groupSpawnDelay);
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        spawning = true;
        currentHealthInc += waveHealthIncrement;
        currentDamageInc += waveAttackIncrement;
        if(GroupNumber % 5 == 0)
        {
            currentDamageInc += waveAttackIncrementAfter5;
            currentHealthInc += waveHealthIncrementAfter5;
            if (canIncreseCost)
            {
                gm.TowerBuildCost += 4;
                canIncreseCost = false;
            }
        }
        else
        {
            canIncreseCost = true;
        }
    }

    public void SetGroup(Group group)
    {
        currentGroup = group;
        GroupFinished = false;
        GroupNumber++;
        Debug.Log("Added");
    }
}
