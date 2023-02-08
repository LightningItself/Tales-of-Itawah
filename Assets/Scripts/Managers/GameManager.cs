using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float towerBuildCost;
    [SerializeField] private Spawner spawner;
    [SerializeField] GameObject gameOverUi;

    public int Score { get; set; }
    public float _Time { get; set; }
    public float TowerBuildCost {
        get
        {
            return towerBuildCost;
        }
        set
        {
            towerBuildCost = value;
        }
    }
    public string Name { get; set; }

    private float enemyEscapeCount = 0;

    public int score;
    public float time;
    public LeaderBoardData saveData;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        saveData = new LeaderBoardData();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = Score;
        time = _Time;
        name = Name;

        if (Input.GetKeyDown(KeyCode.C))
        {
            LeaderBoardSaveSysyem.ClearData();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            saveData = LeaderBoardSaveSysyem.LoadLeaderBoardData();
        }

        if(_Time <= -0.01)
        {
            GameOver();
        }

        GenerateGroup();

        _Time += Time.deltaTime;
    }

    private void GameOver()
    {
        _Time = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
        LeaderBoardSaveSysyem.SaveScore(Name, Score);
    }


    private void GenerateGroup()
    {
        if (spawner.groupFinished)
        {
            Group group = new Group
            {
                enemySpawnDelay = 1,
                enemyCount = spawner.groupNumber * 5,
                enemyIndex = 0,
            };

            spawner.SetGroup(group);
        }
    }

    public void EnemyEscaped()
    {
        enemyEscapeCount++;
        _Time -= enemyEscapeCount * enemyEscapeCount;
    }

    public void TowerBuilt()
    {
        _Time -= towerBuildCost;
        Debug.Log("workin");
    }
}
