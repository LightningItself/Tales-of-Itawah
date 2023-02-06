using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Towers;
    [SerializeField] List<GameObject> TowerMarkers;

    private GameObject selectedTower;
    private GameObject selectedTowerMarker;

    private int selected = -1;

    private void Start()
    {
        selectedTowerMarker = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selected != 0)
            {
                selected = 0;
                //
            }
        }

        if (selected == -1)
        {
            if (selectedTower != null) Destroy(selectedTower);
            selectedTower = null;
        }
    }
}
