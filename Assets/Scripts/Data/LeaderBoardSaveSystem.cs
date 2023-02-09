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

        if (leaderBoardData.data.Count < 10)
        {
            leaderBoardData.data.Add(data);
        }
        else if (leaderBoardData.data[10].score < data.score)
        {
            leaderBoardData.data[10] = data;
        }

        for(int i = leaderBoardData.data.Count - 1; i > 0; i--)
        {
            if(leaderBoardData.data[i].score > leaderBoardData.data[i - 1].score)
            {
                ScoreData t = leaderBoardData.data[i];
                leaderBoardData.data[i] = leaderBoardData.data[i - 1];
                leaderBoardData.data[i - 1] = t;
            }
            else
            {
                break;
            }
        }

        string json = JsonUtility.ToJson(leaderBoardData);

        Debug.Log(json);

        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public static LeaderBoardData LoadLeaderBoardData()
    {
        if (!File.Exists(path)) return new LeaderBoardData(); 
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
