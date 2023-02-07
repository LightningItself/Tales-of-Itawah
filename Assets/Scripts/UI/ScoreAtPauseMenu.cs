using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAtPauseMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Current Score : " + gameManager.score.ToString();
    }
}
