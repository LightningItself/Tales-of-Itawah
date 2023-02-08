using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAndScore : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text score;
    [SerializeField] private Text time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + gameManager.Score.ToString();
        time.text = "Time: " + gameManager._Time.ToString("#.00");

        
    }
}
