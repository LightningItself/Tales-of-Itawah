using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDataComparer : IComparer<ScoreData>
{
    public int Compare(ScoreData x, ScoreData y)
    {
        if (x == null || y == null) return 0;

        if (x.score < y.score) return 1;
        else return 0;
    }
}

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

