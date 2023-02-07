using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class LeaderBoardSaveSysyem
{
    private static string path = Application.persistentDataPath + "/leaderboard.json";

    public static void SaveScore(string name, int score)
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/leaderboard.bin";

        //FileStream stream = new FileStream(path, FileMode.Append);

        //ScoreData data = new ScoreData(name, score);

        //formatter.Serialize(stream, data);
        //stream.Close();
        ScoreData data = new ScoreData(name, score);
        LeaderBoardData leaderBoardData = LoadLeaderBoardData();

        leaderBoardData.data.Add(data);

        string json = JsonUtility.ToJson(leaderBoardData);

        Debug.Log(json);

        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public static LeaderBoardData LoadLeaderBoardData()
    {
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        Debug.Log(json);
        Debug.Log("Hello");

        if (json == "") return new LeaderBoardData();

        LeaderBoardData scores = JsonUtility.FromJson<LeaderBoardData>(json);

        reader.Close();

        return scores;
    }

    public static void ClearData()
    {
        StreamWriter writer = new StreamWriter(path);
        writer.Write("{}");
        writer.Flush();
        writer.Close();
    }
}