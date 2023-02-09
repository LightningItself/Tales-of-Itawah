using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float towerBuildCost; public float ToweBuidCost { get; set; }
    [SerializeField] private Spawner spawner;
    [SerializeField] GameObject gameOverUi;
    [SerializeField] private float timePerUpgrade;

    public int Score { get; set; }
    public float _Time { get; set; }
    public int TowerUpgradeStatus { get; private set; }
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
    public float escCount;
    // Start is called before the first frame update
    void Start()
    {
        saveData = new LeaderBoardData();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        escCount = enemyEscapeCount;
        score = Score;
        time = _Time;
        name = Name;

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    LeaderBoardSaveSysyem.ClearData();
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    saveData = LeaderBoardSaveSysyem.LoadLeaderBoardData();
        //}

        if(_Time <= -0.01)
        {
            GameOver();
        }

        GenerateGroup();

        _Time += Time.deltaTime;
        TowerUpgradeStatus = Mathf.Max((int)(_Time / timePerUpgrade), TowerUpgradeStatus);
        TowerUpgradeStatus = Mathf.Min(TowerUpgradeStatus, 6);
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
        if (spawner.GroupFinished)
        {
            Group group;
            if(spawner.GroupNumber % 3 == 0)
            {
                group = new Group
                {
                    enemySpawnDelay = 1,
                    enemyCount = spawner.GroupNumber / 6,
                    enemyIndex = 1,
                };
            }else if(spawner.GroupNumber % 4 == 0)
            {
                group = new Group
                {
                    enemySpawnDelay = 1,
                    enemyCount = spawner.GroupNumber / 4,
                    enemyIndex = 2,
                };
            }
            else
            {

                group= new Group
                {
                    enemySpawnDelay = 1,
                    enemyCount = 15,
                    enemyIndex = 0,
                };
            }

            spawner.SetGroup(group);
        }
    }

    public void EnemyEscaped()
    {
        enemyEscapeCount++;
        Debug.Log(enemyEscapeCount);
        _Time -= enemyEscapeCount * enemyEscapeCount;
    }

    public void TowerBuilt()
    {
        _Time -= towerBuildCost;
        Debug.Log("workin");
    }
}
