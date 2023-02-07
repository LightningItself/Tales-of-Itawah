using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string name;
    public int score;

    public ScoreData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

[System.Serializable]
public class LeaderBoardData
{
    public List<ScoreData> data;

    public LeaderBoardData()
    {
        data = new List<ScoreData>();
    }
}

