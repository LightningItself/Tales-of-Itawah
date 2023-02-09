using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private List<Text> textList;
    [SerializeField] private GameObject ui;

    private GameObject selected = null;

    public void UpdateText(List<string> statsList, GameObject _selected)
    {
        selected = _selected;
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

    public void ResetText(GameObject _selected)
    {
        if(selected == _selected)
        {
            ui.SetActive(false);
        }
    }
}
