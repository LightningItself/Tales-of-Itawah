using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCostText : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Cost: " + gm.TowerBuildCost;
    }
}
