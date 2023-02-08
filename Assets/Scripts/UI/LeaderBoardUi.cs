using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUi : MonoBehaviour
{
    private List<ScoreData> data;

    [SerializeField] private List<Text> testUis;

    // Start is called before the first frame update
    void Start()
    {
        data = LeaderBoardSaveSysyem.LoadLeaderBoardData().data;

        for(int i = 0, j = data.Count - 1; i < data.Count; i++, j--)
        {
            testUis[i].text = (i + 1).ToString() + ". " + data[j].name + " : " + data[j].score;
        }
    }
}
