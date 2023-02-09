using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private List<Text> textList;
    [SerializeField] private GameObject ui;

    public void UpdateText(List<string> statsList)
    {
        ui.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            if (i + 1 > statsList.Count)
            {
                textList[i].enabled = false;
                continue;
            }

            textList[i].text = statsList[i];
        }
    }

    public void ResetText()
    {
        ui.SetActive(false);
    }
}
