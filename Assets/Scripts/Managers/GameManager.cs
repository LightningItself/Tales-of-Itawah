using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float towerBuildCost;

    public int Score { get; set; }
    public float _Time { get; set; }

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


        if (Input.GetKeyDown(KeyCode.S))
        {
            LeaderBoardSaveSysyem.SaveScore("Test", Score);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            saveData = LeaderBoardSaveSysyem.LoadLeaderBoardData();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LeaderBoardSaveSysyem.ClearData();
        }

        _Time += Time.deltaTime;
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
