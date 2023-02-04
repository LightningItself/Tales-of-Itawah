using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{

    [Serializable]
    public struct EnemyGroup
    {
        public int EnemyIndex { get; set; }
        public int GroupSize { get; set; }
        public int SpawnDelay { get; set; }
    }

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<EnemyGroup> wave;
    [SerializeField] private int groupSpawnDelay;

    private int currentGroup;
    private int totalGroups;

    private bool spawning = true;

    private void Start()
    {
        totalGroups = wave.Count;
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
        int enemyPrefabIndex = wave[currentGroup].EnemyIndex;
        int spawnDelay = wave[currentGroup].SpawnDelay;
        int groupSize = wave[currentGroup].GroupSize;

        Instantiate(enemyPrefabs[enemyPrefabIndex]);

        groupSize--;

        wave[currentGroup] = new EnemyGroup
        {
            EnemyIndex = enemyPrefabIndex,
            SpawnDelay = spawnDelay,
            GroupSize = groupSize,
        };

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


    //private List<GameObject> Pre;
    //private GameObject _poolContainer;


    //private void Awake()
    //{
    //    _pool = new List<GameObject>(); ;
    //    _poolContainer = new GameObject($"Pool - {enemyPrefab.name}");
    //    CreatePooler();
    //}

    //private void CreatePooler()
    //{
    //    for(int i=0;i<poolSize;i++)
    //    {
    //        _pool.Add(CreateInstance());
    //    }
    //}

    //private GameObject CreateInstance()
    //{
    //    GameObject newInstance = Instantiate(enemyPrefab);
    //    newInstance.transform.SetParent(_poolContainer.transform);
    //    newInstance.SetActive((false));
    //    return newInstance;
    //}

    //public GameObject GetInstanceFromPool()
    //{
    //    for(int i=0;i<_pool.Count;i++)
    //    {
    //        if(!_pool[i].activeInHierarchy)
    //        {
    //            return _pool[i];
    //        }
    //    }
    //    return CreateInstance();
    //}

    //public static void ReturnToPool(GameObject instance)
    //{
    //    instance.SetActive(false);
    //}
    //public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    instance.SetActive(false);
    //}
}
